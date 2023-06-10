using System;
using System.Collections.Generic;
using System.Linq;
using _CodeBase.Logging;
using UnityEngine;

namespace _CodeBase.GateCode
{
  public class GatesManager : MonoBehaviour
  {
    private List<Gate> _gates;
    private bool _entered;
    
    private void Start()
    {
      _gates = GetComponentsInChildren<Gate>().ToList();
      SubscribeEvents();
    }

    private void OnDestroy() => UnSubscribeEvents();

    private void SubscribeEvents() => _gates.ForEach(gate => gate.Entered += OnGateEnter);
    private void UnSubscribeEvents() => _gates.ForEach(gate => gate.Entered -= OnGateEnter);

    private void OnGateEnter(Gate gate)
    {
      if(_entered) return;
      _entered = true;
      gate.Impact();
    }
  }
}