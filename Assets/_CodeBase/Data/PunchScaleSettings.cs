using UnityEngine;

namespace _CodeBase.Data
{
  [CreateAssetMenu(fileName = "PunchScaleSettings", menuName = "Settings/PunchScale")]
  public class PunchScaleSettings : ScriptableObject
  {
    public float Punch;
    public float Duration;
    public int Vibrato;
    public float Elasticity;
  }
}