using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDevDemo.Scripts
{
    public class LoadLevelOnOverlap : MonoBehaviour
    {
        [SerializeField]
        private string _levelToLoad;

        private LevelTransitionManager _levelTransitionManager;

        private void Start() => _levelTransitionManager = FindAnyObjectByType<LevelTransitionManager>();

        private void OnTriggerEnter(Collider other)
        {
            if (string.IsNullOrWhiteSpace(_levelToLoad))
            {
                Debug.LogError("No level set", this);
                return;
            }

            if (_levelTransitionManager == null)
            {
                Debug.LogError("Failed to find level transition manager", this);
                return;
            }

            _levelTransitionManager.TransitionToLevel(_levelToLoad);
        }
    }
}