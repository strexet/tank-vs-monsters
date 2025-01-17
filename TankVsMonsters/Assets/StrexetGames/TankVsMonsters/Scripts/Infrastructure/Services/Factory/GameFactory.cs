using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.AssetManagement;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.PersistentProgress;
using UsefulTools.Runtime.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Factory
{
	public class GameFactory : IGameFactory
	{
		private const string InitialPointTag = "InitialPoint";

		private readonly IAssetProvider _assetProvider;
		private readonly List<ISavedProgressReader> _progressReaders = new();
		private readonly List<ISavedProgress> _progressWriters = new();

		private GameObject _player;

		public IReadOnlyList<ISavedProgressReader> ProgressReaders => _progressReaders;

		public IReadOnlyList<ISavedProgress> ProgressWriters => _progressWriters;

		public bool IsPlayerCreated { get; private set; }

		public TransformData PlayerTransformData => _player.transform.GetData();

		public Transform PlayerTransform => _player.transform;

		public event Action PlayerCreated;

		public GameFactory(IAssetProvider assetProvider) => _assetProvider = assetProvider;

		public GameObject CreatePlayer()
		{
			var initialPoint = GameObject.FindWithTag(InitialPointTag);
			var initialTransform = initialPoint.transform;

			_player = InstantiateRegistered(AssetPath.PlayerPrefab, initialTransform.position, initialTransform.rotation);
			IsPlayerCreated = true;
			PlayerCreated?.Invoke();

			return _player;
		}

		public GameObject CreateHud() => InstantiateRegistered(AssetPath.HudPrefab);

		public void CleanUpProgressWatchers()
		{
			_progressReaders.Clear();
			_progressWriters.Clear();
		}

		private GameObject InstantiateRegistered(string prefabPath, Vector3 position, Quaternion rotation)
		{
			var gameObject = _assetProvider.Instantiate(prefabPath, position, rotation);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiateRegistered(string prefabPath)
		{
			var gameObject = _assetProvider.Instantiate(prefabPath);
			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (var progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
			{
				Register(progressReader);
			}
		}

		private void Register(ISavedProgressReader progressReader)
		{
			_progressReaders.Add(progressReader);

			if (progressReader is ISavedProgress progressWriter)
			{
				_progressWriters.Add(progressWriter);
			}
		}
	}
}