using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.GameStates
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;

        public void Enter() { }

        public void Exit() { }
    }
}