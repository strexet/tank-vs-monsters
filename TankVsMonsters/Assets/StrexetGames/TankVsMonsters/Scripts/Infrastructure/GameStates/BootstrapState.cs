using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core.States;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.AssetManagement;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Factory;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Input;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.PersistentProgress;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.SaveLoad;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.GameStates
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
			var inputService = new GameInputService();
			inputService.Enable();
			_services.RegisterSingle<IInputService>(inputService);

			var assetProvider = new AssetProvider();
			_services.RegisterSingle<IAssetProvider>(assetProvider);

			var persistentProgressService = new PersistentProgressService();
			_services.RegisterSingle<IPersistentProgressService>(persistentProgressService);

			var gameFactory = new GameFactory(assetProvider);
			_services.RegisterSingle<IGameFactory>(gameFactory);

			var saveLoadService = new SaveLoadService(persistentProgressService, gameFactory);
			_services.RegisterSingle<ISaveLoadService>(saveLoadService);
		}

		private void OnLevelLoaded() => _gameStateMachine.Enter<LoadProgressState>();
	}
}