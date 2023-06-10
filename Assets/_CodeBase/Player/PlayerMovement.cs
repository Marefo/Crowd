using System;
using _CodeBase.Crowd;
using _CodeBase.Infrastructure.Services;
using _CodeBase.Logging;
using UnityEngine;
using VContainer;

namespace _CodeBase.Player
{
  public class PlayerMovement : MonoBehaviour
  {
    [SerializeField] private Vector3 _moveSpeed;
    [SerializeField] private float _clampXPerUnit;
    [SerializeField] private UnitsCrowd _crowd;
    [SerializeField] private UnitsCrowdAnimator _crowdAnimator;
    
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
      if (_touchedEvenOnce)
        MoveByZ();
      
      if(_isTouching)
        MoveByX();
    }

    private void OnDestroy() => UnSubscribeEvents();

    private void SubscribeEvents()
    {
      _inputService.TouchEntered += OnTouchEnter;
      _inputService.TouchCanceled += OnTouchCancel;
    }

    private void UnSubscribeEvents()
    {
      _inputService.TouchEntered -= OnTouchEnter;
      _inputService.TouchCanceled -= OnTouchCancel;
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
      targetPosition.z += _moveSpeed.z;
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

        float clampX = Mathf.Clamp(_clampXPerUnit - _crowd.CrowdDensity * _crowd.Radius / 2, 0, float.MaxValue);
        move.x = Mathf.Clamp(move.x, -clampX, clampX);

        Vector3 targetPosition = transform.position;
        targetPosition.x = Mathf.Lerp(transform.position.x, move.x, _moveSpeed.x);
        transform.position = targetPosition;
      }
    }
  }
}