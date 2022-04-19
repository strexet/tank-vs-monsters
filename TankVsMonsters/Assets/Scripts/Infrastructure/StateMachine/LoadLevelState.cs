using Infrastructure.Core;
using UI;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        private const string PlayerPrefabPath = "Player/Prefabs/TankPlayer";
        private const string HudPrefabPath = "UI/Prefabs/Hud";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => _loadingCurtain.Hide();

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindWithTag(InitialPointTag);
            var initialTransform = initialPoint.transform;

            Instantiate(PlayerPrefabPath, initialTransform.position, initialTransform.rotation);
            Instantiate(HudPrefabPath);

            _gameStateMachine.Enter<GameLoopState>();
        }

        private static GameObject Instantiate(string prefabPath)
        {
            var prefab = Resources.Load<GameObject>(prefabPath);
            return Object.Instantiate(prefab);
        }

        private static GameObject Instantiate(string prefabPath, Vector3 position, Quaternion rotation)
        {
            var prefab = Resources.Load<GameObject>(prefabPath);
            return Object.Instantiate(prefab, position, rotation);
        }
    }
}