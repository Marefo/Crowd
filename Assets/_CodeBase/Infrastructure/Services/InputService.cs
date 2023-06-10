using System;
using _CodeBase.Logging;
using UnityEngine;
using VContainer.Unity;

namespace _CodeBase.Infrastructure.Services
{
  public class InputService : ITickable
  {
    public event Action TouchEntered;
    public event Action TouchCanceled;
    
    public float TouchDelta { get; private set; }

    private float _lastTouchPosition;
    private float _currentTouchPosition;
    
    public void Tick()
    {
      if (Input.GetMouseButtonDown(0))
      {
        _lastTouchPosition = Input.mousePosition.x;
        TouchEntered?.Invoke();
      }
      else if (Input.GetMouseButton(0))
      {
        _currentTouchPosition = Input.mousePosition.x;
        TouchDelta = _currentTouchPosition - _lastTouchPosition;
        _lastTouchPosition = _currentTouchPosition;
      }
      else if (Input.GetMouseButtonUp(0)) 
        TouchCanceled?.Invoke();
    }
  }
}