using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace _CodeBase.UI
{
  public class FingerHint : MonoBehaviour
  {
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    [Space(10)]
    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _finishPosition;

    private void Start() => Move();

    [Button()]
    private void Move()
    {
      transform.DOKill();
      transform.position = _startPosition.position;
      transform.DOMove(_finishPosition.position, _duration).SetLoops(-1, LoopType.Yoyo).SetEase(_ease)
        .SetLink(gameObject);
    }
  }
}