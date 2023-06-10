using _CodeBase.Coins.Data;
using _CodeBase.PlayerCode;
using UnityEngine;

namespace _CodeBase.Coins
{
  public class Coin : MonoBehaviour
  {
    [SerializeField] private CoinsData _data;
    
    private void OnTriggerEnter(Collider obj)
    {
      if(obj.TryGetComponent(out Player player) == false) return;
      _data.Add();
      Destroy(gameObject);
    }
  }
}