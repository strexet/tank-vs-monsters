using StrexetGames.TankVsMonsters.Scripts.Infrastructure.GameStates;
using StrexetGames.TankVsMonsters.Scripts.UI;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core
{
    public class Game
    {
        public GameStateMachine StateMachine { get; }

        public Game(LoadingCurtain loadingCurtain) =>
            StateMachine = new GameStateMachine(new SceneLoader(), loadingCurtain, ServiceLocator.Container);
    }
}