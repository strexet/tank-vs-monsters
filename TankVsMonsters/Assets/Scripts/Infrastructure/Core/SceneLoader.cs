using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Infrastructure.Core
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void Load(string sceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                return;
            }

            _coroutineRunner.StartCoroutine(LoadSceneRoutine(sceneName, onLoaded));
        }

        private static IEnumerator LoadSceneRoutine(string sceneName, Action onLoaded)
        {
            var waitNextScene = SceneManager.LoadSceneAsync(sceneName);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();
        }
    }
}