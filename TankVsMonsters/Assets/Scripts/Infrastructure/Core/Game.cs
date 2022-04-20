using Infrastructure.Services;
using Infrastructure.States;
using UI;

namespace Infrastructure.Core
{
    public class Game
    {
        public GameStateMachine StateMachine { get; }

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain) =>
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, ServiceLocator.Container);
    }
}