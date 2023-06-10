using System;
using _CodeBase.Crowd;
using _CodeBase.Infrastructure;
using UnityEngine;
using VContainer;

namespace _CodeBase.PlayerCode
{
  public class Player : MonoBehaviour
  {
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private UnitsCrowd _crowd;
    [SerializeField] private UnitsCrowdAnimator _crowdAnimator;

    private GameState _gameState;

    [Inject]
    private void Construct(GameState gameState)
    {
      _gameState = gameState;
      SubscribeEvents();
    }

    private void OnDestroy() => UnSubscribeEvents();

    private void SubscribeEvents()
    {
      _gameState.Won += OnWin;
      _crowd.UnitsAmountBecomeZero += OnUnitsAmountBecomeZero;
    }

    private void UnSubscribeEvents()
    {
      _gameState.Won -= OnWin;
      _crowd.UnitsAmountBecomeZero -= OnUnitsAmountBecomeZero;
    }

    private void OnWin()
    {
      _movement.Disable();
      _crowdAnimator.PlayDance();
    }

    private void OnUnitsAmountBecomeZero()
    {
      _movement.Disable();
      _gameState.Lose();
    }
  }
}