﻿using System;
using System.Collections.Generic;
using System.Linq;
using _CodeBase.Logging;
using _CodeBase.UnitCode;
using UnityEngine;

namespace _CodeBase.Crowd
{
  public class UnitsCrowd : MonoBehaviour
  {
    public List<Unit> EnabledUnits => _allUnits.Where(unit => unit.Enabled).ToList();
    public int EnabledUnitsAmount => EnabledUnits.Count;
    public float CrowdDensity => (float) EnabledUnitsAmount / (float) _maxUnitsAmount;
    public float Radius => _radius;

    [SerializeField] private int _startUnitsAmount;
    [SerializeField] private int _maxUnitsAmount;
    [Space(10)]
    [SerializeField] private float _distanceBetweenUnits;
    [SerializeField] private float _radius;
    [Space(10)]
    [SerializeField] private Transform _unitsParent;
    [Space(10)]
    [SerializeField] private Unit _unitPrefab;

    private readonly List<Unit> _allUnits = new List<Unit>();
    private List<Unit> _disabledUnits => _allUnits.Where(unit => unit.Enabled == false).ToList();

    private void Start() => SpawnUnits();

    private void SpawnUnits()
    {
      for (int i = 0; i < _maxUnitsAmount; i++) 
        SpawnUnit(i <= _startUnitsAmount - 1);
      
      UpdateUnitsPosition();
    }

    private void SpawnUnit(bool enabled = false)
    {
      Unit unit = Instantiate(_unitPrefab, _unitsParent);
      
      if(enabled == false)
        unit.Disable();
      
      _allUnits.Add(unit);
    }

    public void AddUnits(int amount)
    {
      for (int i = 0; i < amount; i++) 
        AddUnit();
      
      UpdateUnitsPosition();
    }

    public void HideUnits(int amount)
    {
      for (int i = 0; i < amount; i++) 
        HideUnit();
      
      UpdateUnitsPosition();
    }

    public void HideUnit(Unit unit = null)
    {
      bool isCertainUnit = unit != null;

      if (unit == null)
        unit = EnabledUnits.Last();
      
      unit.Disable();
      
      if(isCertainUnit)
        UpdateUnitsPosition();
    }

    private void AddUnit()
    {
      _disabledUnits.First().Enable();
    }

    private void UpdateUnitsPosition()
    {
      for (int i = 1; i < EnabledUnitsAmount; i++)
      {
        float x = _distanceBetweenUnits * Mathf.Sqrt(i) * Mathf.Cos(i * _radius);
        float z = _distanceBetweenUnits * Mathf.Sqrt(i) * Mathf.Sin(i * _radius);
            
        Vector3 newUnitPosition = new Vector3(x,0,z);
        EnabledUnits[i].transform.localPosition = newUnitPosition;
      }
    }
  }
}