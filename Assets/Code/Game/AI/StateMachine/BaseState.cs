using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.AI
{
    public abstract class BaseState
    {
        private List<StateTransition> _transitions = new List<StateTransition>();

        public List<StateTransition> Transitions { get { return _transitions; } }

        public abstract void Update();

        public BaseState AddTransition(StateTransition transition)
        {
            _transitions.Add(transition);
            return this;
        }
    }
}