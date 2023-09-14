using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDevDemo.Scripts
{
    public class LevelTransitionManager : MonoBehaviour
    {
        [field: SerializeField]
        public string LoadingSceneName { get; private set; }

        [field: SerializeField]
        public float LoadingSceneDelay { get; private set; } = 3.0f;
    
        private void Start() => DontDestroyOnLoad(this);

        public void TransitionToLevel(string level)
        {
            var loadingScreenOp = SceneManager.LoadSceneAsync(LoadingSceneName, LoadSceneMode.Additive);
            loadingScreenOp.completed += async op =>
            {
                var activeSceneName = SceneManager.GetActiveScene().name;
                var unloadActiveOp = SceneManager.UnloadSceneAsync(activeSceneName);
                unloadActiveOp.completed += _ =>
                {
                    Debug.Log($"Unloaded level {activeSceneName}");
                };
                
                await Awaitable.WaitForSecondsAsync(LoadingSceneDelay);

                var loadNewLevelOp = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
                loadNewLevelOp.completed += _ =>
                {
                  var unloadLoadingScreenOp = SceneManager.UnloadSceneAsync(LoadingSceneName);
                    unloadLoadingScreenOp.completed += _ =>
                    {
                        Debug.Log("Unloaded loading level");
                    };
                        
                    SceneManager.SetActiveScene(SceneManager.GetSceneByName(level));
                };
            };
        }
    }
}
