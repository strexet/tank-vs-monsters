using System;
using System.Collections.Generic;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UsefulTools.Runtime.Extensions;

namespace Infrastructure.Services.Factory
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