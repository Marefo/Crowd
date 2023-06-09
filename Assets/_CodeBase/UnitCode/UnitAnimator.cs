using UnityEngine;

namespace _CodeBase.UnitCode
{
  public class UnitAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;

    private readonly int _isRunningHash = Animator.StringToHash("IsRunning");

    public void ChangeRunState(bool isRunning) => _animator.SetBool(_isRunningHash, isRunning);
  }
}