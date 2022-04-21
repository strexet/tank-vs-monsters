using System;
using Extensions;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        bool IsPlayerCreated { get; }
        TransformData PlayerTransformData { get; }
        Transform PlayerTransform { get; }
        event Action PlayerCreated;

        GameObject CreatePlayer();
        GameObject CreateHud();
    }
}