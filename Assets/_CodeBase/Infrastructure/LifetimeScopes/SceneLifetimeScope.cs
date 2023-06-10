using _CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _CodeBase.Infrastructure.LifetimeScopes
{
  public class SceneLifetimeScope : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      builder.Register<SceneService>(Lifetime.Singleton);
    }
  }
}