using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Inputs;
using System;
using UnityEngine;

namespace ArenaShooter.AI
{
    public class BossBrain : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField]
        private float _longDistanceAttack = 14f;
        [SerializeField]
        private float _cooldownLongDistanceAttack = 3f;
        [SerializeField]
        private float _targetedAttackDistance = 7f;
        [SerializeField]
        private float _cooldownTargetedAttack = 5f;
        [SerializeField]
        private float _areaAttackDistance = 3f;
        [SerializeField]
        private float _cooldownAreaAttack = 5f;

        [SerializeField]
        private float _pursueDistance = 20f;

        private bool _isAttackPhase = false;
        private bool _isPursuePhase = false;

        private Transform _target;

        public event Action OnAreaAttack;
        public event Action OnTargetedAttack;
        public event Action OnLongDistanceAttack;
        public event Action<Vector2> OnMove;

        private PlayerScannerComponent _scanner;
        private CircleTrigger2DComponent _circleTrigger;

        private void Start()
        {
            _circleTrigger = GetComponent<CircleTrigger2DComponent>();
            _scanner = GetComponent<PlayerScannerComponent>();
            _scanner.OnPlayerDetected += OnPlayerDetected;
            _scanner.OnPlayerLost += OnPlayerLost;
            _scanner.Construct(_circleTrigger);
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
            if (_target == null) return;
            if (_isAttackPhase) return;

            var distanceToTarget = Vector2.Distance(transform.position, _target.position);
            


            if (distanceToTarget < _pursueDistance && distanceToTarget > _longDistanceAttack)
            {
                var desiredVelocity = (_target.position - transform.position).normalized;

                OnMove?.Invoke(desiredVelocity);
                Debug.Log("Move to target");
            }
            else if(distanceToTarget < _longDistanceAttack && distanceToTarget > _targetedAttackDistance)
            {
                OnLongDistanceAttack?.Invoke();
                Debug.Log("Attack with long distance");
            }
            else if(distanceToTarget < _targetedAttackDistance && distanceToTarget > _areaAttackDistance)
            {
                OnTargetedAttack?.Invoke();
                Debug.Log("Attack with targeted");
            }
            else if(distanceToTarget <= _areaAttackDistance)
            {
                OnAreaAttack?.Invoke();
                Debug.Log("Attack with area");
            }
            else
            {
                OnMove?.Invoke(Vector2.zero);
                Debug.Log("Stay on position");
            }
        }

        public void OnPlayerDetected(GameObject target)
        {
            _target = target.transform;
        }

        public void OnPlayerLost(GameObject target)
        {
            _target = null;
        }
    }
}