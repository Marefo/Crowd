using _CodeBase.Infrastructure;
using _CodeBase.Infrastructure.Services;
using Unity.VisualScripting;
using VContainer;

namespace _CodeBase.UI.Screens
{
  public class HintScreen : Screen
  {
    private GameState _gameState;
    private InputService _inputService;

    [Inject]
    private void Construct(InputService inputService)
    {
      _inputService = inputService;
      _inputService.TouchEntered += FastClose;
    }

    private void OnDestroy()
    {
      _inputService.TouchEntered -= FastClose;
    }
  }
}