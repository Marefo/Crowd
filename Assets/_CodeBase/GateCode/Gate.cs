using System;
using _CodeBase.Crowd;
using _CodeBase.Data;
using TMPro;
using UnityEngine;

namespace _CodeBase.GateCode
{
  public class Gate : MonoBehaviour
  {
    public event Action<Gate> Entered; 

    [Range(0, 100), SerializeField] private int _impactValue;
    [SerializeField] private CrowdImpactType _impactType;
    [Space(10)] 
    [SerializeField] private GameObject _visual;
    [SerializeField] private TextMeshProUGUI _textField;

    private UnitsCrowd _unitsCrowd;
    private bool _used;
    
    private void Start() => InitializeText();

    private void OnTriggerEnter(Collider other)
    {
      if(_used || other.TryGetComponent(out UnitsCrowd unitsCrowd) == false) return;
      _unitsCrowd = unitsCrowd;
      Entered?.Invoke(this);
      _used = true;
    }

    public void Impact()
    {
      switch (_impactType)
      {
        case CrowdImpactType.Add:
          Add(_unitsCrowd);
          break;
        case CrowdImpactType.Multiply:
          Multiply(_unitsCrowd);
          break;
      }
      
      _visual.SetActive(false);
    }

    private void Add(UnitsCrowd unitsCrowd) => unitsCrowd.AddUnits(_impactValue);

    private void Multiply(UnitsCrowd unitsCrowd) => unitsCrowd.Multiply(_impactValue);

    private void InitializeText()
    {
      string text = "";
      
      switch (_impactType)
      {
        case CrowdImpactType.Add:
          text = $"+{_impactValue}";
          break;
        case CrowdImpactType.Multiply:
          text = $"x{_impactValue}";
          break;
      }

      _textField.text = text;
    }
  }
}