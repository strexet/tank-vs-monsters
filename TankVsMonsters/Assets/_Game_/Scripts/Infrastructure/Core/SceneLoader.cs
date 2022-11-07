using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.Core
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

            LoadSceneAsync(sceneName, onLoaded, onProgress);
        }

        private static async UniTaskVoid LoadSceneAsync(string sceneName, Action onLoaded, Action<float> onProgress)
        {
            await SceneManager.LoadSceneAsync(sceneName).ToUniTask(
                Progress.Create<float>(progress => onProgress?.Invoke(progress))
            );

            onLoaded?.Invoke();
        }
    }
}