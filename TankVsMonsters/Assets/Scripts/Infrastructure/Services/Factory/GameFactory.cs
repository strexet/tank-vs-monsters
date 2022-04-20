using Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider) => _assetProvider = assetProvider;

        public GameObject CreatePlayer()
        {
            var initialPoint = GameObject.FindWithTag(InitialPointTag);
            var initialTransform = initialPoint.transform;

            return _assetProvider.Instantiate(AssetPath.PlayerPrefab, initialTransform.position, initialTransform.rotation);
        }

        public GameObject CreateHud() => _assetProvider.Instantiate(AssetPath.HudPrefab);
    }
}