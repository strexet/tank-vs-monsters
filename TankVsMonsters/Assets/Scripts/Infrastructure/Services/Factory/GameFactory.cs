using System;
using Extensions;
using Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly IAssetProvider _assetProvider;
        private GameObject _player;

        public bool IsPlayerCreated { get; private set; }
        public TransformData PlayerTransformData => _player.transform.GetTransformData();
        public Transform PlayerTransform => _player.transform;
        public event Action PlayerCreated;

        public GameFactory(IAssetProvider assetProvider) => _assetProvider = assetProvider;

        public GameObject CreatePlayer()
        {
            var initialPoint = GameObject.FindWithTag(InitialPointTag);
            var initialTransform = initialPoint.transform;

            _player = _assetProvider.Instantiate(AssetPath.PlayerPrefab, initialTransform.position, initialTransform.rotation);
            IsPlayerCreated = true;
            PlayerCreated?.Invoke();

            return _player;
        }

        public GameObject CreateHud() => _assetProvider.Instantiate(AssetPath.HudPrefab);
    }
}