using UnityEngine;

namespace _CodeBase.Crowd.Data
{
  [CreateAssetMenu(fileName = "UnitsCrowdSettings", menuName = "Settings/UnitsCrowd")]
  public class UnitsCrowdSettings : ScriptableObject
  {
    public float DistanceBetweenUnits;
    public float Radius;
    public float CrowdColliderRadiusMultiplier;
  }
}