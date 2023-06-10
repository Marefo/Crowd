using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _CodeBase.Logic
{
  [RequireComponent(typeof(Collider))]
  public class TriggerListener : MonoBehaviour
  {
    public event Action<Collider> Entered;
    public event Action<Collider> Canceled;

    public readonly List<Collider> CollidersInZone = new List<Collider>();

    public bool IsInZone(GameObject obj)
    {
      CollidersInZone.RemoveAll(colliderFromZone => colliderFromZone == null);
      return CollidersInZone.Any(colliderFromZone => colliderFromZone.gameObject == obj);
    }

    private void OnTriggerEnter(Collider other)
    {
      CollidersInZone.Add(other);
      Entered?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
      CollidersInZone.Remove(other);
      Canceled?.Invoke(other);
    }
  }
}