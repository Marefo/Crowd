using _CodeBase.Infrastructure;
using UnityEngine;

namespace _CodeBase.UI.Screens
{
  public class WinScreen : Screen
  {
    private void OnEnable() => _gameState.Won += OpenWithDelay;
    private void OnDisable() => _gameState.Won -= OpenWithDelay;
  }
}