using System;
using _CodeBase.UnitCode;
using UnityEngine;

namespace _CodeBase.Logic
{
  public class Obstacle : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out Unit unit) == false) return;
      unit.GetCrowd().HideUnit(true, unit);
    }
  }
}