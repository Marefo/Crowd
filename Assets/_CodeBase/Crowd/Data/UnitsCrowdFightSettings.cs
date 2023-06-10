using UnityEngine;

namespace _CodeBase.Crowd.Data
{
  [CreateAssetMenu(fileName = "UnitsCrowdFightSettings", menuName = "Settings/UnitsCrowdFight")]
  public class UnitsCrowdFightSettings : ScriptableObject
  {
    public float MoveSpeed;
    public float FightingUnitsDistance;
    public float UnitKillTime;
  }
}