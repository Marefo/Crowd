using System;
using System.Collections;
using System.Linq;
using _CodeBase.Crowd.Data;
using _CodeBase.Logging;
using _CodeBase.Logic;
using _CodeBase.UnitCode;
using DG.Tweening;
using NaughtyAttributes.Test;
using UnityEngine;

namespace _CodeBase.Crowd
{
  public class UnitsCrowdFighter : MonoBehaviour
  {
    public event Action FightStarted;
    public event Action LostFight;
    public event Action WonFight;

    public int UnitsAmount => _crowd.EnabledUnits.Count;
    
    [SerializeField] private UnitsCrowd _crowd;
    [SerializeField] private UnitsCrowdAnimator _crowdAnimator;
    [SerializeField] private TriggerListener _crowdZone;
    [Space(10)]
    [SerializeField] private UnitsCrowdFightSettings _settings;

    private UnitsCrowdFighter _enemyCrowd;
    private int _unitsAmountAfterFight;
    private Tween _updateUnitsPositionTween;
    
    private void OnEnable()
    {
      _crowdZone.Entered += OnCrowdZoneEnter;
      _crowd.UnitsAmountBecomeZero += LoseFight;
    }

    private void OnDisable()
    {
      _crowdZone.Entered -= OnCrowdZoneEnter;
      _crowd.UnitsAmountBecomeZero -= LoseFight;
    }

    private void OnDestroy()
    {
      if(_enemyCrowd != null)
        _enemyCrowd.LostFight -= WinFight;
    }

    private void OnCrowdZoneEnter(Collider obj)
    {
      if(obj.TryGetComponent(out UnitsCrowdFighter crowdFighter) == false) return;

      StartFight(crowdFighter);
      _enemyCrowd.StartFight(this);
    }

    public Unit GetFirstUnit() => _crowd.EnabledUnits.First();
    public Unit GetUnit(int index) => _crowd.EnabledUnits[index];

    private void StartFight(UnitsCrowdFighter enemyCrowd)
    {
      _unitsAmountAfterFight = Mathf.Clamp(UnitsAmount - enemyCrowd.UnitsAmount, 0, int.MaxValue);
      
      if(_enemyCrowd != null)
        _enemyCrowd.LostFight -= WinFight;
      
      _enemyCrowd = enemyCrowd;

      _crowdAnimator.PlayRun();
      FightStarted?.Invoke();
      _enemyCrowd.LostFight += WinFight;
      StartCoroutine(FightCoroutine());
    }

    private void LoseFight() => LostFight?.Invoke();

    private void WinFight()
    {
      _updateUnitsPositionTween?.Kill();
      _crowdAnimator.PlayIdle();
      ResetUnitsRotation();
      _updateUnitsPositionTween = DOVirtual.DelayedCall(0.2f, _crowd.UpdateUnitsPosition).SetLink(gameObject);
      WonFight?.Invoke();
    }

    private IEnumerator FightCoroutine()
    {
      float time = 0;
      
      while (_crowd.EnabledUnits.Count > _unitsAmountAfterFight)
      {
        if (_enemyCrowd.UnitsAmount > 0)
        {
          RotateUnitsToEnemyCrowd();
          MoveAllyUnitToEnemy();
        }

        if (time >= _settings.UnitKillTime)
        {
          _crowd.HideUnit(false);
          time = 0;
        }
        else
          time += Time.deltaTime;
        
        yield return null;
      }
    }

    private void MoveAllyUnitToEnemy()
    {
      for (int i = 0; i < _crowd.EnabledUnits.Count; i++)
      {
        Unit enemyFirstUnit = _enemyCrowd.GetFirstUnit();
        Unit currentAllyUnit = GetUnit(i);
        float distance = Vector3.Distance(enemyFirstUnit.transform.position, currentAllyUnit.transform.position);

        if (distance >= _settings.FightingUnitsDistance)
        {
          Vector3 targetPosition = enemyFirstUnit.transform.position;
          targetPosition.y = currentAllyUnit.transform.position.y;
          
          currentAllyUnit.transform.position = Vector3.Lerp(currentAllyUnit.transform.position,targetPosition, 
             _settings.MoveSpeed * Time.deltaTime);
        }
      }
    }
    
    private void ResetUnitsRotation() => 
      _crowd.EnabledUnits.ForEach(unit => unit.transform.rotation = Quaternion.identity);

    private void RotateUnitsToEnemyCrowd()
    {
      Vector3 enemyPositionOnSameHeight = _enemyCrowd.transform.position;
      enemyPositionOnSameHeight.y = transform.position.y;
      Vector3 enemyDirection = enemyPositionOnSameHeight - transform.position;

      foreach (Unit unit in _crowd.EnabledUnits)
      {
        Quaternion targetRotation = Quaternion.LookRotation(enemyDirection, Vector3.up);
        unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, targetRotation, Time.deltaTime * 3f);
      }
    }
  }
}