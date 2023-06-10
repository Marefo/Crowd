using System;
using System.Collections.Generic;
using _CodeBase.Data;
using _CodeBase.Infrastructure.Services;
using _CodeBase.Logging;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace _CodeBase.Infrastructure
{
  public class Bootstrapper : MonoBehaviour
  {
    [SerializeField] private string _loadLevel;
    [SerializeField] private List<GameObject> _indestructibleObjects;
    
    private const string BootstrapSceneName = "Bootstrap";
    
    private SceneService _sceneService;

    [Inject]
    private void Construct(SceneService sceneService)
    {
      _sceneService = sceneService;
    }

    private void Awake() => Initialize();

#if UNITY_EDITOR
    private void OnEnable() => EditorApplication.playModeStateChanged += LogPlayModeState;
    private void OnDisable() => EditorApplication.playModeStateChanged -= LogPlayModeState;

    private void LogPlayModeState(PlayModeStateChange obj)
    {
      if(obj != PlayModeStateChange.ExitingPlayMode) return;
      MarkAsUnBootstrapped();
    }
#endif

    private void Initialize()
    {
      if (gameObject.scene.name == BootstrapSceneName)
      {
        Application.targetFrameRate = 60;
        MarkAsBootstrapped();
        _indestructibleObjects.ForEach(DontDestroyOnLoad);
        string loadLevel = GetSavedLevel();
        _sceneService.LoadScene(loadLevel);
      }
      else
      {
        if (IsBootstrapped() == false) 
          SceneManager.LoadScene(BootstrapSceneName);
        else
          SaveLevel();
      } 
    }

    private bool IsBootstrapped()
    {
      bool bootstrapped = false;
      
      if(PlayerPrefs.HasKey(SaveKeys.WasBootstrappedKey))
        bootstrapped = PlayerPrefs.GetInt(SaveKeys.WasBootstrappedKey) == 1;

      return bootstrapped;
    }

    private void MarkAsBootstrapped() => PlayerPrefs.SetInt(SaveKeys.WasBootstrappedKey, 1);
    private void MarkAsUnBootstrapped() => PlayerPrefs.SetInt(SaveKeys.WasBootstrappedKey, 0);

    private void SaveLevel() => PlayerPrefs.SetString(SaveKeys.LevelKey, gameObject.scene.name);
    private string GetSavedLevel()
    {
      string level = _loadLevel;
      
      if(PlayerPrefs.HasKey(SaveKeys.LevelKey))
        level = PlayerPrefs.GetString(SaveKeys.LevelKey, gameObject.scene.name);

      return level;
    }
  }
}