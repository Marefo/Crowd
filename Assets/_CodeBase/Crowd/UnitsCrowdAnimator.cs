using UnityEngine;

namespace _CodeBase.Crowd
{
  public class UnitsCrowdAnimator : MonoBehaviour
  {
    [SerializeField] private UnitsCrowd _unitsCrowd;

    private string _currentAnimation;
    private string _idleAnimation = "idle";
    private string _runAnimation = "run";
    private string _danceAnimation = "dance";

    public void UpdateUnitsAnimation()
    {
      _unitsCrowd.EnabledUnits.ForEach(
        unit => unit.Animators.ForEach(animator => animator.Play(_currentAnimation)));
    }
    
    public void PlayIdle()
    {
      _currentAnimation = _idleAnimation;
      _unitsCrowd.EnabledUnits.ForEach(
        unit => unit.Animators.ForEach(animator => animator.Play(_idleAnimation)));
    }

    public void PlayRun()
    {
      _currentAnimation = _runAnimation;
      _unitsCrowd.EnabledUnits.ForEach(
        unit => unit.Animators.ForEach(animator => animator.Play(_runAnimation)));
    }

    public void PlayDance()
    {
      _currentAnimation = _danceAnimation;
      _unitsCrowd.EnabledUnits.ForEach(
        unit => unit.Animators.ForEach(animator => animator.Play(_danceAnimation)));
    }
  }
}