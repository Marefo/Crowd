namespace _CodeBase.UI.Buttons
{
  public class NextLevelButton : ButtonUI
  {
    protected override void OnClick() => _sceneService.LoadNextScene();
  }
}