using Infrastructure.Services;
using Infrastructure.States;
using UI;

namespace Infrastructure.Core
{
    public class Game
    {
        public GameStateMachine StateMachine { get; }

        public Game(LoadingCurtain loadingCurtain) =>
            StateMachine = new GameStateMachine(new SceneLoader(), loadingCurtain, ServiceLocator.Container);
    }
}