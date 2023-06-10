using System;
using _CodeBase.Infrastructure.Services;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _CodeBase.UI
{
  public class HintPanel : MonoBehaviour
  {
    private InputService _inputService;

    [Inject]
    private void Construct(InputService inputService)
    {
      _inputService = inputService;
      _inputService.TouchEntered += OnTouchEnter;
    }

    private void OnDestroy() => _inputService.TouchEntered -= OnTouchEnter;

    private void OnTouchEnter() => gameObject.SetActive(false);
  }
}