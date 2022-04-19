using Infrastructure.Core;
using Services.Input;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private const string MainScene = "Main";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(InitialScene, EnterMainLevel);
        }

        private void EnterMainLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(MainScene);
        }

        private void RegisterServices()
        {
            RegisterInputService();
        }

        public void Exit()
        {
        }
        
        private static void RegisterInputService()
        {
            var inputService = new GameInputService();
            inputService.Enable();
            Game.InputService = inputService;
        }
    }
}