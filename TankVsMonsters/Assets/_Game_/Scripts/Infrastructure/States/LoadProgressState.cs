using Actors.Data;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoad;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoad)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoad = saveLoad;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PlayerPositionOnLevel.LevelName);
        }

        public void Exit() { }

        private void LoadProgressOrInitNew()
        {
            var loadedProgress = _saveLoad.LoadProgress();
            var playerProgress = loadedProgress ?? NewProgress();
            _progressService.Progress = playerProgress;
        }

        private static PlayerProgress NewProgress() => new("Main");
    }
}