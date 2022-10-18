using Infrastructure.States;
using UI;
using UnityEngine;

namespace Infrastructure.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;

        private Game _game;

        private void Awake()
        {
            var loadingCurtain = Instantiate(_loadingCurtainPrefab);

            _game = new Game(loadingCurtain);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}