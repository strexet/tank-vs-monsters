using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core
{
    public class SceneLoader
    {
        public void Load(string sceneName, Action onLoaded = null, Action<float> onProgress = null)
        {
            if (string.Equals(sceneName, SceneManager.GetActiveScene().name))
            {
                onLoaded?.Invoke();
                return;
            }

            LoadSceneAsync(sceneName, onLoaded, onProgress).Forget();
        }

        private static async UniTaskVoid LoadSceneAsync(string sceneName, Action onLoaded, Action<float> onProgress)
        {
            if (onProgress != null)
            {
                await SceneManager.LoadSceneAsync(sceneName)
                   .ToUniTask(Progress.Create(onProgress));
            }
            else
            {
                await SceneManager.LoadSceneAsync(sceneName)
                   .ToUniTask();
            }

            onLoaded?.Invoke();
        }
    }
}