using System;
using _CodeBase.Data;
using UnityEngine;
using VContainer.Unity;

namespace _CodeBase.Coins.Data
{
  [CreateAssetMenu(fileName = "CoinsData", menuName = "StaticData/Coins")]
  public class CoinsData : ScriptableObject, IStartable
  {
    public event Action<int> AmountChanged;
    
    public int Amount { get; private set; }

    public void Start() => Load();

    public void Add()
    {
      Amount += 1;
      AmountChanged?.Invoke(Amount);
      Save();
    }

    private void Save() => PlayerPrefs.SetInt(SaveKeys.MoneyAmountKey, Amount);

    private void Load()
    {
      if(PlayerPrefs.HasKey(SaveKeys.MoneyAmountKey)) return;
      Amount = PlayerPrefs.GetInt(SaveKeys.MoneyAmountKey);
    }
  }
}