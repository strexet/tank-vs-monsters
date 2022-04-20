using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer();
        GameObject CreateHud();
    }
}