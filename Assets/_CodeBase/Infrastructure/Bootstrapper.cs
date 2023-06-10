using System.Collections.Generic;
using _CodeBase.Infrastructure.Services;
using UnityEngine;
using VContainer;

namespace _CodeBase.Infrastructure
{
  public class Bootstrapper : MonoBehaviour
  {
    [SerializeField] private List<GameObject> _indestructibleObjects;
    
    private const string BootstrapSceneName = "Bootstrap";
    private const string FirstLevelSceneName = "Level-1";
    
    private SceneService _sceneService;

    [Inject]
    private void Construct(SceneService sceneService)
    {
      _sceneService = sceneService;
    }
    
    private void Awake() => Initialize();

    private void Initialize()
    {
      _indestructibleObjects.ForEach(DontDestroyOnLoad);
      _sceneService.LoadNextScene();
    }
  }
}