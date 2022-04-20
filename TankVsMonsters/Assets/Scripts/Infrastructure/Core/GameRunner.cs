using UnityEngine;

namespace Infrastructure.Core
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _bootstrapperPrefab;

        private void Awake()
        {
            var bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper == null)
            {
                Instantiate(_bootstrapperPrefab);
            }
        }
    }
}