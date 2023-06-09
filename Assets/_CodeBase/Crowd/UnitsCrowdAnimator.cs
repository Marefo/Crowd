using UnityEngine;

namespace _CodeBase.Crowd
{
  public class UnitsCrowdAnimator : MonoBehaviour
  {
    [SerializeField] private UnitsCrowd _unitsCrowd;

    public void ChangeRunState(bool isRunning) =>
      _unitsCrowd.EnabledUnits.ForEach(unit => unit.Animator.ChangeRunState(isRunning));
  }
}