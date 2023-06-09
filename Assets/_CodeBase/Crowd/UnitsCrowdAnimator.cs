using UnityEngine;

namespace _CodeBase.Crowd
{
  public class UnitsCrowdAnimator : MonoBehaviour
  {
    [SerializeField] private UnitsCrowd _unitsCrowd;

    private string _idleAnimation = "idle";
    private string _runAnimation = "run";
    private string _danceAnimation = "dance";

    public void PlayIdle() => _unitsCrowd.EnabledUnits.ForEach(unit => unit.Animator.Play(_idleAnimation));
    public void PlayRun() => _unitsCrowd.EnabledUnits.ForEach(unit => unit.Animator.Play(_runAnimation));
    public void PlayDance() => _unitsCrowd.EnabledUnits.ForEach(unit => unit.Animator.Play(_danceAnimation));
  }
}