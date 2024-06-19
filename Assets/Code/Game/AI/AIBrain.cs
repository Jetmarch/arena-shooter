using ArenaShooter.Components;
using ArenaShooter.Inputs;
using System.Collections;
using UnityEngine;

namespace ArenaShooter.AI
{
    public class AIBrain : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField]
        private float _pursueDistance = 10f;
        [SerializeField]
        private float _attackDistance = 7f;


        private AIInputController _inputController;

        private bool _isAttacking;
        private int _attackCount = 3;
        private float _timeBetweenAttacks = 1f;


        private PlayerScannerComponent _playerScanner;
        private Transform _target;

        public void Construct(AIInputController inputController, PlayerScannerComponent playerScanner)
        {
            _inputController = inputController;
            _playerScanner = playerScanner;

            _playerScanner.OnPlayerDetected += OnPlayerDetected;
            _playerScanner.OnPlayerLost += OnPlayerLost;
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
            if (_target == null) return;
            _inputController.WorldMouseMove(_target.position);
            _inputController.ScreenMouseMove(_target.position);

            if (_isAttacking) return;

            var distanceToTarget = Vector2.Distance(transform.position, _target.position);

            if (distanceToTarget < _pursueDistance && distanceToTarget > _attackDistance)
            {
                var desiredVelocity = (_target.position - transform.position).normalized;

                _inputController.Move(desiredVelocity);
            }
            else if (distanceToTarget <= _attackDistance)
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
            _inputController.Reload();
            _isAttacking = false;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}