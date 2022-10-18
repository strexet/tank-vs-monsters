using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.Core
{
    public class SceneLoader
    {
        public void Load(string sceneName, Action onLoaded = null)
        {
            if (string.Equals(sceneName, SceneManager.GetActiveScene().name))
            {
                onLoaded?.Invoke();
                return;
            }

            LoadSceneAsync(sceneName, onLoaded);
        }

        private static async UniTaskVoid LoadSceneAsync(string sceneName, Action onLoaded)
        {
            await SceneManager.LoadSceneAsync(sceneName).ToUniTask(
                // Progress.Create<float>(x => Debug.Log(x))
            );

            onLoaded?.Invoke();
        }
    }
}