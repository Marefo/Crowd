using System;
using System.Collections;
using _CodeBase.Crowd;
using _CodeBase.Infrastructure.Services;
using _CodeBase.Logging;
using _CodeBase.Player.Data;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace _CodeBase.Player
{
  public class PlayerMovement : MonoBehaviour
  {
    [SerializeField] private UnitsCrowd _crowd;
    [SerializeField] private UnitsCrowdAnimator _crowdAnimator;
    [SerializeField] private UnitsCrowdFighter _crowdFighter;
    [Space(10)] 
    [SerializeField] private PlayerMovementSettings _settings;

    private bool _enabled = true;
    private InputService _inputService;
    private Camera _camera;
    private Plane _plane;
    private bool _isTouching;
    private bool _touchedEvenOnce;
    private Vector3 _touchStartWorldPosition;
    private Vector3 _playerPositionOnTouchStart;

    private void Awake()
    {
      _camera = Camera.main;
      _plane = new Plane(Vector3.up, 0);
    }

    [Inject]
    private void Construct(InputService inputService)
    {
      _inputService = inputService;
      SubscribeEvents();
    }

    private void FixedUpdate()
    {
      if(_enabled == false) return;
      
      if(_touchedEvenOnce)
        MoveByZ();
      
      if(_isTouching)
        MoveByX();
    }

    private void OnDestroy() => UnSubscribeEvents();

    private void SubscribeEvents()
    {
      _inputService.TouchEntered += OnTouchEnter;
      _inputService.TouchCanceled += OnTouchCancel;
      _crowdFighter.FightStarted += Disable;
      _crowdFighter.WonFight += Enable;
    }

    private void UnSubscribeEvents()
    {
      _inputService.TouchEntered -= OnTouchEnter;
      _inputService.TouchCanceled -= OnTouchCancel;
      _crowdFighter.FightStarted -= Disable;
      _crowdFighter.WonFight -= Enable;
    }
    
    public void Enable()
    {
      _enabled = true;
      _crowdAnimator.PlayRun();
    }

    public void Disable()
    {
      _enabled = false;
      OnTouchCancel();
    }

    private void OnTouchEnter()
    {
      if (_touchedEvenOnce == false)
      {
        _touchedEvenOnce = true;
        _crowdAnimator.PlayRun();
      }

      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
      if (_plane.Raycast(ray,out float distance))
      {
        _touchStartWorldPosition = ray.GetPoint(distance + 1f);
        _playerPositionOnTouchStart = transform.position;
      }

      _isTouching = true;
    }

    private void OnTouchCancel() => _isTouching = false;

    private void MoveByZ()
    {
      Vector3 targetPosition = transform.position;
      targetPosition.z += _settings.MoveSpeed.z;
      transform.position = targetPosition;
    }

    private void MoveByX()
    {
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

      if (_plane.Raycast(ray, out float distance))
      {
        Vector3 currentTouchWorldPosition = ray.GetPoint(distance + 1f);
        Vector3 touchWorldDelta = currentTouchWorldPosition - _touchStartWorldPosition;
        Vector3 move = _playerPositionOnTouchStart + touchWorldDelta;

        float clampX = Mathf.Clamp(_settings.ClampXPerUnit - _crowd.CrowdDensity * _crowd.Radius / 2, 0, float.MaxValue);
        move.x = Mathf.Clamp(move.x, -clampX, clampX);

        Vector3 targetPosition = transform.position;
        targetPosition.x = Mathf.Lerp(transform.position.x, move.x, _settings.MoveSpeed.x);
        transform.position = targetPosition;
      }
    }
  }
}