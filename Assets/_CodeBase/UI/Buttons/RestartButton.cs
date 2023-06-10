namespace _CodeBase.UI.Buttons
{
  public class RestartButton : ButtonUI
  {
    protected override void OnClick() => _sceneService.ReloadCurrentScene();
  }
}