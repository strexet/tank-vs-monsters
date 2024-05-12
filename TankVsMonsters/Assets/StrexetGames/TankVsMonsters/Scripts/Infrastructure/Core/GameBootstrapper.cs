using StrexetGames.TankVsMonsters.Scripts.Infrastructure.GameStates;
using StrexetGames.TankVsMonsters.Scripts.UI;
using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingCurtain _loadingCurtainPrefab;

        private Game _game;

        private void Awake()
        {
#if UNITY_EDITOR
            var bootstrappers = FindObjectsOfType<GameBootstrapper>();

            if (bootstrappers.Length != 1)
            {
                Debug.LogWarning($"[MORE THAN ONE] <b><color=red>{nameof(GameBootstrapper)}.{nameof(Awake)}></color></b> "
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