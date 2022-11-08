using System;
using System.Collections.Generic;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.Factory;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.PersistentProgress;
using StrexetGames.TankVsMonsters.Scripts.Infrastructure.Services.SaveLoad;
using StrexetGames.TankVsMonsters.Scripts.UI;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, ServiceLocator services) =>
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(),
                    services.Single<ISaveLoadService>()),
                [typeof(LoadLevelState)] = new LoadLevelState(this, services, sceneLoader, loadingCurtain, services.Single<IGameFactory>(),
                    services.Single<IPersistentProgressService>()),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };

        public void Enter<TState>() where TState : IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : IExitableState
        {
            _activeState?.Exit();

            var state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : IExitableState => (TState)_states[typeof(TState)];
    }
}