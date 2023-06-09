using System.Collections.Generic;
using UnityEngine;

namespace _CodeBase.UnitCode
{
  public class Unit : MonoBehaviour
  {
    public bool Enabled { get; private set; } = true;
    
    [field: SerializeField] public List<AnimatedMesh> Animators { get; private set; }
    [field: Space(10)]
    [SerializeField] private GameObject _model;

    public void Enable()
    {
      _model.SetActive(true);
      Enabled = true;
    }

    public void Disable()
    {
      _model.SetActive(false);
      Enabled = false;
    }
  }
}