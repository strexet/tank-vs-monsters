using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core.States;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Factory;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.PersistentProgress;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.SaveLoad;
using StrexetGames.TankVsMonsters.Scripts.UI;
using System;
using System.Collections.Generic;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.GameStates
{
    public class GameStateMachine : StateMachine
    {
        private GameStateMachine() { }

        public static GameStateMachine Create(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, ServiceLocator services)
        {
            var stateMachine = new GameStateMachine();

            stateMachine.SetupStates(new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(stateMachine, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(stateMachine,
                    services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(stateMachine, services, sceneLoader, loadingCurtain,
                    services.Single<IGameFactory>(), services.Single<IPersistentProgressService>()),
                [typeof(GameLoopState)] = new GameLoopState(stateMachine)
            });

            return stateMachine;
        }
    }
}