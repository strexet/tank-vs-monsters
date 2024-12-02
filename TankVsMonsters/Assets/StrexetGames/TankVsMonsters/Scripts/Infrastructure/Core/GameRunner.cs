using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core
{
	public class GameRunner : MonoBehaviour
	{
		[SerializeField] private GameBootstrapper _bootstrapperPrefab;

		private void Awake()
		{
			var bootstrapper = FindAnyObjectByType<GameBootstrapper>();

			if (bootstrapper == null)
			{
				Instantiate(_bootstrapperPrefab);
			}
		}
	}
}