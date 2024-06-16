using ArenaShooter.Components;
using ArenaShooter.Inputs;
using ArenaShooter.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.AI
{
    public class AIBrain : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField]
        private AIType _type;
        private StateMachine _stateMachine;
        private AIStateMachineFactory _factory;

        private AIInputController _inputController;
        
        public void Construct(AIInputController inputController, AIStateMachineFactory stateMachineFactory)
        {
            _inputController = inputController;
            _factory = stateMachineFactory;
        }

        private void Start()
        {
            _stateMachine = _factory.CreateStateMachine(_type, _inputController, transform);
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }

        public void OnUpdate(float delta)
        {
            _stateMachine.Update();
        }
    }
}