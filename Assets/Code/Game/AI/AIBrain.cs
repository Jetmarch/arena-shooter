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

        private Transform _target;
        private AIInputController _inputController;

        [Inject]
        private void Construct(Transform player, AIStateMachineFactory stateMachineFactory)
        {
            _target = player;
            _factory = stateMachineFactory;
        }

        private void Start()
        {
            _inputController = GetComponent<AIInputController>();
            _stateMachine = _factory.CreateStateMachine(_type, _inputController, transform, _target);
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