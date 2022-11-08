using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string prefabPath, Vector3 position, Quaternion rotation);
        GameObject Instantiate(string prefabPath);
    }
}