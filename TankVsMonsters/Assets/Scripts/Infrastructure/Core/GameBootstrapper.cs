using Infrastructure.States;
using UI;
using UnityEngine;

namespace Infrastructure.Core
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;

        private Game _game;

        private void Awake()
        {
            var loadingCurtain = Instantiate(_loadingCurtainPrefab);
            
            _game = new Game(this, loadingCurtain);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}