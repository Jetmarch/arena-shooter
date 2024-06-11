using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.AI
{
    public class StateMachine : MonoBehaviour
    {
        private List<IState> _states;

        private IState _currentState;

        public IState CurrentState { get { return _currentState; } }
        public event Action<IState> StateChanged;

        public StateMachine()
        {
            _states = new List<IState>();
        }

        public void AddState(IState state)
        {
            _states.Add(state);
        }

        public void SetCurrentState(IState state)
        {
            _currentState = state;
        }

        public void UpdateCurrentState()
        {
            _currentState.Update();
        }
    }
}