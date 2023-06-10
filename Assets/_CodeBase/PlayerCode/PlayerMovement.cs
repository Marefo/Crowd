using System;
using _CodeBase.Crowd;
using _CodeBase.Infrastructure.Services;
using _CodeBase.PlayerCode.Data;
using UnityEngine;
using VContainer;

namespace _CodeBase.PlayerCode
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

    private void Update()
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
      
      _isTouching = true;
    }

    private void OnTouchCancel() => _isTouching = false;

    private void MoveByZ()
    {
      Vector3 targetPosition = transform.position;
      targetPosition.z += _settings.MoveSpeed.z * Time.deltaTime;
      transform.position = targetPosition;
    }

    private void MoveByX()
    {
      float moveX = Mathf.Clamp(_inputService.TouchDelta * _settings.MoveSpeed.x * Time.deltaTime, 
          -_settings.MaxMovePerTimeX, _settings.MaxMovePerTimeX);
      
      float clampX = Mathf.Clamp(_settings.ClampXPerUnit - _crowd.CrowdDensity * _crowd.Radius / 2, 0, float.MaxValue);
      moveX = Mathf.Clamp(transform.position.x + moveX, -clampX, clampX);

      Vector3 targetPosition = transform.position;
      targetPosition.x = moveX;
      transform.position = targetPosition;
    }
  }
}