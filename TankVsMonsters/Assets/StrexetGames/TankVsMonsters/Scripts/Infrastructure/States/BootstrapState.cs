using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.AssetManagement;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Factory;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Input;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.PersistentProgress;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.SaveLoad;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";

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

        public void Enter() => _sceneLoader.Load(InitialScene, OnLevelLoaded);

        public void Exit() { }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(CreateInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

            _services.RegisterSingle<IGameFactory>(
                new GameFactory(_services.Single<IAssetProvider>())
            );

            _services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(_services.Single<IPersistentProgressService>(),
                    _services.Single<IGameFactory>())
            );
        }

        private void OnLevelLoaded() => _gameStateMachine.Enter<LoadProgressState>();

        private static GameInputService CreateInputService()
        {
            var inputService = new GameInputService();
            inputService.Enable();
            return inputService;
        }
    }
}