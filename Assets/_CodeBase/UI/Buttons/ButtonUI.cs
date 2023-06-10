using _CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _CodeBase.UI.Buttons
{
  public abstract class ButtonUI : MonoBehaviour
  {
    [SerializeField] private Button _button;

    protected SceneService _sceneService;

    [Inject]
    private void Construct(SceneService sceneService)
    {
      _sceneService = sceneService;
    }

    protected virtual void OnEnable() => _button.onClick.AddListener(OnClick);
    protected virtual void OnDisable() => _button.onClick.RemoveListener(OnClick);

    protected abstract void OnClick();
  }
}