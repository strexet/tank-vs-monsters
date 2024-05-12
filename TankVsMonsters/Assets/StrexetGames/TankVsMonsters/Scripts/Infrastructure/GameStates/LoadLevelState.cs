using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core.States;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Factory;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.PersistentProgress;
using StrexetGames.TankVsMonsters.Scripts.UI;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ServiceLocator _services;

        public LoadLevelState(GameStateMachine gameStateMachine, ServiceLocator services, SceneLoader sceneLoader,
            LoadingCurtain loadingCurtain, IGameFactory gameFactory, IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _persistentProgressService = persistentProgressService;
            _services = services;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();

            _services.DisposeSceneServices();
            _gameFactory.CleanUpProgressWatchers();

            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => _loadingCurtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InitGameWorld()
        {
            _gameFactory.CreatePlayer();
            _gameFactory.CreateHud();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_persistentProgressService.Progress);
            }
        }
    }
}