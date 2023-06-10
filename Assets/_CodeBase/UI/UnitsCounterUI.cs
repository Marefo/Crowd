using System;
using _CodeBase.Crowd;
using TMPro;
using UnityEngine;

namespace _CodeBase.UI
{
  public class UnitsCounterUI : MonoBehaviour
  {
    [SerializeField] private UnitsCrowd _crowd;
    [SerializeField] private GameObject _visual;
    [SerializeField] private TextMeshProUGUI _textField;

    private void OnEnable() => _crowd.UnitsAmountChanged += OnUnitsAmountChange;
    private void OnDisable() => _crowd.UnitsAmountChanged -= OnUnitsAmountChange;

    private void OnUnitsAmountChange(int amount)
    {
      if (amount == 0)
      {
        _visual.SetActive(false);  
        return;
      }
      
      _textField.text = amount.ToString();
    }
  }
}