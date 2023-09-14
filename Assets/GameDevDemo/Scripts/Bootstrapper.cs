using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDevDemo.Scripts
{
    public class Bootstrapper : MonoBehaviour {
        [SerializeField]
        private SceneToLoad[] _scenesToLoad = Array.Empty<SceneToLoad>();
        
        [SerializeField]
        private string _bootstrapperSceneName = "Root";
        
        [SerializeField]
        private string _playerSceneName = "Player";

        private readonly List<string> _loadedScenes = new();
        private readonly List<string> _initiallyLoadedScenes = new();
        
        private void Start()
        {
            for (var i = 0; i < SceneManager.loadedSceneCount; i++)
            {
                _initiallyLoadedScenes.Add(SceneManager.GetSceneAt(i).name);
            }

            if (!_initiallyLoadedScenes.Contains(_playerSceneName))
            {
                var loadingOp = SceneManager.LoadSceneAsync(_playerSceneName, LoadSceneMode.Additive);
                loadingOp.completed += _ => { LoadDesiredScenes(); };
            }
            else
            {
                LoadDesiredScenes();
            }
        }

        private void LoadDesiredScenes()
        {

            foreach (var scene in _scenesToLoad)
            {
                var isSceneLoaded = _initiallyLoadedScenes.Contains(scene.Name);
                if (isSceneLoaded)
                {
                    Debug.Log($"Found {scene.Name} already loaded, ignoring");
                    _loadedScenes.Add(scene.Name);
                    continue;
                }
                
                var loadingOp = SceneManager.LoadSceneAsync(scene.Name, LoadSceneMode.Additive);
                loadingOp.completed += _ =>
                {
                    if (scene.IsActive)
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.Name));
                    }

                    ReportLoaded(scene.Name);
                };
            }
        }

        private void ReportLoaded(string sceneName)
        {
            _loadedScenes.Add(sceneName);

            if (_loadedScenes.SequenceEqual(_scenesToLoad.Select(s => s.Name)))
            {
                var unloadingOp = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(_bootstrapperSceneName));
                unloadingOp.completed += _ =>
                {
                    Debug.Log($"Unloading bootstrapper {_bootstrapperSceneName}");
                };
            }
        }

        [Serializable]
        private struct SceneToLoad
        {
            public string Name;
            public bool IsActive;
        }
    }
}