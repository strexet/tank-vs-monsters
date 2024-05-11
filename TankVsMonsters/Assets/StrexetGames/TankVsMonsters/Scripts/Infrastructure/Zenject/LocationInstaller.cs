using StrexetGames.TankVsMonsters.Scripts.Actors.Player;
using UnityEngine;
using Zenject;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Zenject
{
    public class LocationInstaller : MonoInstaller
    {
        public Transform StartPoint;
        public GameObject HeroPrefab;

        public override void InstallBindings()
        {
            BindHero();
        }

        private void BindHero()
        {
            var playerMove = Container.InstantiatePrefabForComponent<PlayerMove>(
                HeroPrefab,
                StartPoint.position,
                Quaternion.identity,
                null
            );

            Container
               .Bind<PlayerMove>()
               .FromInstance(playerMove)
               .AsSingle();
        }
    }
}