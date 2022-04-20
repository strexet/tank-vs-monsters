using Infrastructure.Core;
using Infrastructure.Services;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Input;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private const string MainScene = "Main";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter() => _sceneLoader.Load(InitialScene, EnterMainLevel);

        public void Exit() { }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(CreateInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
        }

        private void EnterMainLevel() => _gameStateMachine.Enter<LoadLevelState, string>(MainScene);

        private static GameInputService CreateInputService()
        {
            var inputService = new GameInputService();
            inputService.Enable();
            return inputService;
        }
    }
}