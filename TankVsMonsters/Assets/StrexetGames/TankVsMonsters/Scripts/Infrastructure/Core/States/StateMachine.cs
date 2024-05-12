using System;
using System.Collections.Generic;

namespace StrexetGames.TankVsMonsters.Scripts.Infrastructure.Core.States
{
    public abstract class StateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;
        protected void SetupStates(Dictionary<Type, IExitableState> states) => _states = states;

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