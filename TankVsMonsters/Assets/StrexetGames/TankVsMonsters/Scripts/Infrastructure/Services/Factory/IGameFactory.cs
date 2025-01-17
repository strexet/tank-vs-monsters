using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;
using UsefulTools.Runtime.Extensions;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Factory
{
	public interface IGameFactory : IService
	{
		bool IsPlayerCreated { get; }

		TransformData PlayerTransformData { get; }

		Transform PlayerTransform { get; }

		IReadOnlyList<ISavedProgressReader> ProgressReaders { get; }

		IReadOnlyList<ISavedProgress> ProgressWriters { get; }

		event Action PlayerCreated;

		GameObject CreatePlayer();

		GameObject CreateHud();

		void CleanUpProgressWatchers();
	}
}