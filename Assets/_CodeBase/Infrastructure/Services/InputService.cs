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
    
    public void Tick()
    {
      if (Input.GetMouseButtonDown(0))
        TouchEntered?.Invoke();
      else if (Input.GetMouseButtonUp(0)) 
        TouchCanceled?.Invoke();
    }
  }
}