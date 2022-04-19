using Infrastructure.StateMachine;
using Services.Input;
using UI;

namespace Infrastructure.Core
{
    public class Game
    {
        public static IInputService InputService { get; set; }
        public GameStateMachine StateMachine { get; }

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain) =>
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain);
    }
}