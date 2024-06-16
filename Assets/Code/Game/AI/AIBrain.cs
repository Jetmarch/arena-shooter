using ArenaShooter.Components;
using ArenaShooter.Inputs;
using ArenaShooter.Units;
using ArenaShooter.Units.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.AI
{
    public class AIBrain : MonoBehaviour, IGameUpdateListener
    {
        //[SerializeField]
        //private AIType _type;
        //private StateMachine _stateMachine;
        private AIStateMachineFactory _factory;

        private AIInputController _inputController;


        private bool _isAttacking;
        private int _attackCount = 3;
        private float _timeBetweenAttacks = 1f;

        private PlayerScannerComponent _playerScanner;
        private Transform _target;
        //TODO: Собрать отдельный объект, сканирующий в радиусе определенные цели
        public void Construct(AIInputController inputController, AIStateMachineFactory stateMachineFactory, PlayerScannerComponent playerScanner)
        {
            _inputController = inputController;
            _factory = stateMachineFactory;
            _playerScanner = playerScanner;

            _playerScanner.OnPlayerDetected += OnPlayerDetected;
            _playerScanner.OnPlayerLost += OnPlayerLost;
        }

        private void Start()
        {
            //_stateMachine = _factory.CreateStateMachine(_type, _inputController, transform);
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);

            if (_playerScanner == null) return;
            _playerScanner.OnPlayerDetected += OnPlayerDetected;
            _playerScanner.OnPlayerLost += OnPlayerLost;
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);

            if (_playerScanner == null) return;
            _playerScanner.OnPlayerDetected -= OnPlayerDetected;
            _playerScanner.OnPlayerLost -= OnPlayerLost;
        }

        private void OnPlayerLost(GameObject player)
        {
            _target = null;
            Debug.Log("Target lost");
        }

        private void OnPlayerDetected(GameObject player)
        {
            _target = player.transform;

            Debug.Log("Target detected");
        }

        public void OnUpdate(float delta)
        {
            //_stateMachine.Update();

            if (_target == null) return;
            if (_isAttacking) return;

            var distanceToTarget = Vector2.Distance(transform.position, _target.position);

            if (distanceToTarget < 4f && distanceToTarget > 2f)
            {
                var desiredVelocity = (_target.position - transform.position).normalized;

                _inputController.Move(desiredVelocity);
            }
            else if(distanceToTarget <= 2f)
            {
                StartCoroutine(Attack());
            }
            else
            {
                _inputController.Move(Vector2.zero);
            }
        }

        private IEnumerator Attack()
        {
            _isAttacking = true;
            _inputController.Move(Vector2.zero);

            for (int i = 0; i < _attackCount; i++)
            {
                _inputController.Shoot();
                Debug.Log("Attacking");
                yield return new WaitForSeconds(_timeBetweenAttacks);
            }

            _isAttacking = false;
        }
    }
}