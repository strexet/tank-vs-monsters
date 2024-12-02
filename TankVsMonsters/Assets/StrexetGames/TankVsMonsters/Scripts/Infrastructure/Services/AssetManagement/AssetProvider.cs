using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.AssetManagement
{
	public class AssetProvider : IAssetProvider
	{
		public GameObject Instantiate(string prefabPath, Vector3 position, Quaternion rotation)
		{
			var prefab = Resources.Load<GameObject>(prefabPath);
			return Object.Instantiate(prefab, position, rotation);
		}

		public GameObject Instantiate(string prefabPath)
		{
			var prefab = Resources.Load<GameObject>(prefabPath);
			return Object.Instantiate(prefab);
		}
	}
}