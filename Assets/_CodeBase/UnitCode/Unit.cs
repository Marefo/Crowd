using System.Collections.Generic;
using _CodeBase.Crowd;
using UnityEngine;

namespace _CodeBase.UnitCode
{
  public class Unit : MonoBehaviour
  {
    public bool Enabled { get; private set; } = true;
    
    [field: SerializeField] public List<AnimatedMesh> Animators { get; private set; }
    [field: Space(10)]
    [SerializeField] private GameObject _visual;

    private UnitsCrowd _unitsCrowd;
    
    public void Initialize(UnitsCrowd unitsCrowd) => _unitsCrowd = unitsCrowd;

    public UnitsCrowd GetCrowd() => _unitsCrowd;

    public void Enable()
    {
      _visual.SetActive(true);
      Enabled = true;
    }

    public void Disable()
    {
      _visual.SetActive(false);
      Enabled = false;
    }
  }
}