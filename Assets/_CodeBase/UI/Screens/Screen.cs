using _CodeBase.Infrastructure;
using _CodeBase.UI.Screens.Data;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace _CodeBase.UI.Screens
{
  public class Screen : MonoBehaviour
  {
    public bool IsVisible => _visual.localScale != Vector3.zero;

    [SerializeField] private bool _visibleOnStart;
    [SerializeField] private Transform _visual;
    [Space(10)]
    [SerializeField] private ScreenSettings _settings;

    protected GameState _gameState;
    private Tween _openWithDelayTween;

    [Inject]
    private void Construct(GameState gameState)
    {
      _gameState = gameState;
      _gameState.LevelAwake += OnLevelAwake;
    }

    private void OnDestroy() => _gameState.LevelAwake -= OnLevelAwake;

    private void OnLevelAwake()
    {
      if (_visibleOnStart)
        FastOpen();
      else
        FastClose();
    }

    public virtual void OpenWithDelay()
    {
      _openWithDelayTween?.Kill();
      _openWithDelayTween = DOVirtual.DelayedCall(_settings.ShowDelay, Open).SetLink(gameObject);
    }

    public virtual void FastOpen()
    {
      _visual.DOKill();
      _visual.localScale = Vector3.one;
    }
    
    public virtual void Open()
    {
      _visual.DOKill();
      _visual.DOScale(Vector3.one, 0.15f)
        .SetUpdate(true)
        .SetLink(gameObject);
    }

    public virtual void FastClose()
    {
      _visual.DOKill();
      _visual.localScale = Vector3.zero;
    }

    public virtual void Close()
    {
      _visual.DOKill();
      _visual.DOScale(Vector3.zero, 0.15f)
        .SetUpdate(true)
        .SetLink(gameObject);
    }
  }
}