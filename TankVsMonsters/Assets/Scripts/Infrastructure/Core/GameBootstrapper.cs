using Infrastructure.StateMachine;
using UI;
using UnityEngine;

namespace Infrastructure.Core
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;

        private Game _game;

        private void Awake()
        {
            _game = new Game(this, _loadingCurtain);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}