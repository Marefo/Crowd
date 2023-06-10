using _CodeBase.Coins.Data;
using _CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace _CodeBase.Infrastructure.LifetimeScopes
{
  public class GameLifetimeScope : LifetimeScope
  {
    [FormerlySerializedAs("_moneyData")] [SerializeField] private CoinsData coinsData;
    
    protected override void Configure(IContainerBuilder builder)
    {
      builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
      builder.RegisterInstance(coinsData).AsImplementedInterfaces();
      builder.Register<GameState>(Lifetime.Singleton);
      builder.Register<SceneService>(Lifetime.Scoped);
    }
  }
}