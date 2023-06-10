using System;
using _CodeBase.Logging;
using UnityEngine;

namespace _CodeBase.Infrastructure
{
  public class GameState
  {
    public event Action LevelAwake;
    public event Action LevelStarted;
    public event Action LevelDestroyed;
    public event Action Won;
    public event Action Lost;

    public void OnLevelAwake() => LevelAwake?.Invoke();
    public void OnLevelStart() => LevelStarted?.Invoke();
    public void OnLevelDestroy() => LevelDestroyed?.Invoke();
    
    public void Win() => Won?.Invoke();
    public void Lose() => Lost?.Invoke();
  }
}