using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services;
using System;
using System.Collections.Generic;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core
{
    public enum Lifetime
    {
        App = 0,
        Scene = 100
    }

    public class ServiceLocator
    {
        private static ServiceLocator instance;

        private readonly Dictionary<Type, IService> _singletonServices;
        private readonly Dictionary<Type, IService> _sceneServices;

        private readonly List<Dictionary<Type, IService>> _servicesList;

        public static ServiceLocator Container => instance ??= new ServiceLocator();

        private ServiceLocator()
        {
            _singletonServices = new Dictionary<Type, IService>();
            _sceneServices = new Dictionary<Type, IService>();

            _servicesList = new List<Dictionary<Type, IService>>
            {
                _singletonServices,
                _sceneServices
            };
        }

        public void DisposeSceneServices() =>
            _sceneServices.Clear();

        public TService Single<TService>() where TService : IService
        {
            foreach (var specificServices in _servicesList)
            {
                if (specificServices.ContainsKey(typeof(TService)))
                {
                    return (TService)specificServices[typeof(TService)];
                }
            }

            return default;
        }

        public void RegisterSingle<TService>(TService implementation, Lifetime serviceLifetime = Lifetime.App) where TService : IService
        {
            switch (serviceLifetime)
            {
                case Lifetime.Scene:
                    _sceneServices[typeof(TService)] = implementation;
                    break;

                case Lifetime.App:
                default:
                    _singletonServices[typeof(TService)] = implementation;
                    break;
            }
        }
    }
}