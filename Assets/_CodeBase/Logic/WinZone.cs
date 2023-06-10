using System;
using _CodeBase.Infrastructure;
using _CodeBase.PlayerCode;
using UnityEngine;
using VContainer;

namespace _CodeBase.Logic
{
  public class WinZone : MonoBehaviour
  {
    private GameState _gameState;
    
    [Inject]
    private void Construct(GameState gameState)
    {
      _gameState = gameState;
    }
    
    private void OnTriggerEnter(Collider other)
    {
      if(other.TryGetComponent(out Player player) == false) return;
      _gameState.Win();
    }
  }
}