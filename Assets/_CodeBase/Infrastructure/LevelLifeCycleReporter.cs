using System;
using UnityEngine;
using VContainer;

namespace _CodeBase.Infrastructure
{
  public class LevelLifeCycleReporter : MonoBehaviour
  {
    private GameState _gameState;
    
    [Inject]
    private void Construct(GameState gameState)
    {
      _gameState = gameState;
    }

    private void Awake() => _gameState.OnLevelAwake();
    private void Start() => _gameState.OnLevelStart();
    private void OnDestroy() => _gameState.OnLevelDestroy();
  }
}