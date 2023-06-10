using _CodeBase.Infrastructure.Services;
using VContainer;
using VContainer.Unity;

namespace _CodeBase.Infrastructure.LifetimeScopes
{
  public class GameLifetimeScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
      builder.Register<SceneService>(Lifetime.Singleton);
      builder.Register<GameState>(Lifetime.Singleton);
    }
  }
}