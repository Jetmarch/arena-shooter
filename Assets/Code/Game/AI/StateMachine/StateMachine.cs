using System;
using System.Collections.Generic;

namespace ArenaShooter.AI
{
    public class StateMachine
    {
        private List<BaseState> _states;

        private BaseState _currentState;

        public BaseState CurrentState { get { return _currentState; } }
        public event Action<BaseState> StateChanged;

        public StateMachine()
        {
            _states = new List<BaseState>();
        }

        public void AddState(BaseState state)
        {
            _states.Add(state);
        }

        public void SetCurrentState(BaseState state)
        {
            _currentState = state;
        }

        public void Update()
        {
            CheckStateTransitions();
            _currentState.Update();
        }

        private void CheckStateTransitions()
        {
            foreach (var transition in _currentState.Transitions)
            {
                if (transition.IsConditionMet())
                {
                    _currentState = transition.NextState;
                    StateChanged?.Invoke(_currentState);
                }
            }
        }
    }
}