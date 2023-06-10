using System;
using _CodeBase.Coins.Data;
using _CodeBase.Data;
using _CodeBase.Infrastructure;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace _CodeBase.UI
{
  public class CoinsCounter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _textField;
    [Space(10)]
    [SerializeField] private CoinsData _coinsData;
    [SerializeField] private PunchScaleSettings _punchScaleSettings;

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
      _coinsData.AmountChanged += ChangeNumber;
      _gameState.LevelAwake += OnLevelAwake;
    }
    
    private void UnSubscribeEvents()
    {
      _coinsData.AmountChanged -= ChangeNumber;
      _gameState.LevelAwake -= OnLevelAwake;
    }
    
    private void OnLevelAwake()
    {
      UpdateText();
    }

    private void UpdateText() => ChangeNumber(_coinsData.Amount);

    private void ChangeNumber(int newNumber)
    {
      _textField.text = newNumber.ToString();
      _textField.transform.DOPunchScale(Vector3.one * _punchScaleSettings.Punch, _punchScaleSettings.Duration,
        _punchScaleSettings.Vibrato, _punchScaleSettings.Elasticity).SetLink(_textField.gameObject);
    }
  }
}