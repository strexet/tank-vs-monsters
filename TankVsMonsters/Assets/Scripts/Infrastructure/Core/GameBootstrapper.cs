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
#if UNITY_EDITOR
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper != null)
            {
                Debug.LogWarning($"[MORE THAN ONE]<b><color=red>{nameof(GameBootstrapper)}.{nameof(Awake)}></color></b> "
                                 + $"There is more than one {nameof(GameBootstrapper)}!");
            }
#endif

            var loadingCurtain = Instantiate(_loadingCurtainPrefab);

            _game = new Game(loadingCurtain);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}