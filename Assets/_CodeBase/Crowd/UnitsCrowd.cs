using System;
using System.Collections.Generic;
using System.Linq;
using _CodeBase.Crowd.Data;
using _CodeBase.Logging;
using _CodeBase.UnitCode;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _CodeBase.Crowd
{
  public class UnitsCrowd : MonoBehaviour
  {
    public event Action<int> UnitsAmountChanged;
    public event Action UnitsAmountBecomeZero;

    public List<Unit> EnabledUnits => _allUnits.Where(unit => unit.Enabled).ToList();
    public float CrowdDensity => (float) EnabledUnits.Count / (float) _maxUnitsAmount;
    public float Radius => _settings.Radius;

    [SerializeField] private int _startUnitsAmount;
    [SerializeField] private int _maxUnitsAmount;
    [Space(10)]
    [SerializeField] private Unit _unitPrefab;
    [Space(10)]
    [SerializeField] private Transform _unitsParent;
    [SerializeField] private UnitsCrowdAnimator _crowdAnimator;
    [SerializeField] private SphereCollider _crowdCollider;
    [Space(10)]
    [SerializeField] private UnitsCrowdSettings _settings;

    private readonly List<Unit> _allUnits = new List<Unit>();
    private List<Unit> _disabledUnits => _allUnits.Where(unit => unit.Enabled == false).ToList();
    private Tween _updateUnitsPositionTween;
    
    private void Start() => SpawnUnits();

    private void SpawnUnits()
    {
      for (int i = 0; i < _maxUnitsAmount; i++) 
        SpawnUnit(i <= _startUnitsAmount - 1);
      
      _crowdAnimator.PlayIdle();
      UpdateUnitsPosition();
      UnitsAmountChanged?.Invoke(EnabledUnits.Count);
    }

    private void SpawnUnit(bool enabled = false)
    {
      Unit unit = Instantiate(_unitPrefab, _unitsParent);
      unit.Initialize(this);
      
      if(enabled == false)
        unit.Disable();
      
      _allUnits.Add(unit);
    }

    public void UpdateUnitsPosition()
    {
      for (int i = 1; i < EnabledUnits.Count; i++)
      {
        float x = _settings.DistanceBetweenUnits * Mathf.Sqrt(i) * Mathf.Cos(i * _settings.Radius);
        float z = _settings.DistanceBetweenUnits * Mathf.Sqrt(i) * Mathf.Sin(i * _settings.Radius);
            
        Vector3 newUnitPosition = new Vector3(x,0,z);
        Unit unit = EnabledUnits[i];
        unit.transform.DOKill();
        unit.transform.DOLocalMove(newUnitPosition, 0.8f).SetEase(Ease.OutBack).SetLink(unit.gameObject);
      }
      
      UpdateCrowdCollider();
    }

    public void Multiply(int multiplier)
    {
      int targetAmount = EnabledUnits.Count * multiplier;
      int difference = targetAmount - EnabledUnits.Count;
      AddUnits(difference);
    }

    public void AddUnits(int amount)
    {
      for (int i = 0; i < amount; i++) 
        AddUnit();
      
      UpdateUnitsPosition();
      _crowdAnimator.UpdateUnitsAnimation();
    }

    public void HideUnits(int amount)
    {
      for (int i = 0; i < amount; i++) 
        HideUnit();
    }

    public void HideUnit(bool withPositionUpdate = true, Unit unit = null)
    {
      if (unit == null)
        unit = EnabledUnits.First();
      
      unit.Disable();

      UnitsAmountChanged?.Invoke(EnabledUnits.Count);

      if (EnabledUnits.Count == 0)
      {
        UnitsAmountBecomeZero?.Invoke();
        return;
      }

      if (withPositionUpdate == false) return;
      
      _updateUnitsPositionTween?.Kill();
      _updateUnitsPositionTween = DOVirtual.DelayedCall(.3f, UpdateUnitsPosition);
    }

    private void AddUnit()
    {
      _disabledUnits.First().Enable();
      UnitsAmountChanged?.Invoke(EnabledUnits.Count);
    }

    private void UpdateCrowdCollider()
    {
      float maxUnitDistance = 0f;

      for (int i = 1; i < EnabledUnits.Count; i++)
      {
        float unitDistance = _settings.DistanceBetweenUnits * Mathf.Sqrt(i);

        if (unitDistance > maxUnitDistance) 
          maxUnitDistance = unitDistance;
      }
      
      float sphereColliderRadius = maxUnitDistance * Mathf.Sin(_settings.Radius);
      _crowdCollider.radius = sphereColliderRadius * _settings.CrowdColliderRadiusMultiplier;
    }
  }
}