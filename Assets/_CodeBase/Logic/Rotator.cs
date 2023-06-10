using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace _CodeBase.Logic
{
  public class Rotator : MonoBehaviour
  {
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _duration;
    
    private void Start() => Rotate();

    [Button()]
    private void Rotate()
    {
      transform.DOKill();
      transform.DORotate(_rotation, _duration).SetLoops(-1).SetEase(Ease.Linear).SetLink(gameObject);
    }
  }
}