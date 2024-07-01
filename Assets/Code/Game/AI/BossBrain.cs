using System;
using UnityEngine;

namespace ArenaShooter.AI
{
    public class BossBrain : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField]
        private float _radiusOfInteraction = 3f;
        [SerializeField]
        private float _maxMovePhaseTime = 3f;

        private bool _isAttackPhase = false;
        private bool _isMovePhase = false;

        private Vector3 _desiredPosition;
        private float _movePhaseTime;

        private Transform _target;

        public event Action OnAttack;
        public event Action<Vector2> OnMove;
        public event Action<Vector2> LookAt;

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
            if (_target == null || _isAttackPhase)
            {
                OnMove?.Invoke(Vector2.zero);
                return;
            }

            LookAt?.Invoke(_target.position);

            if (!_isMovePhase)
            {
                var rndInCirclePosition = UnityEngine.Random.insideUnitCircle * _radiusOfInteraction;
                var positionNearTarget = new Vector3(_target.position.x + rndInCirclePosition.x, _target.position.y + rndInCirclePosition.y, 0f);
                _desiredPosition = (positionNearTarget - transform.position).normalized;

                _isMovePhase = true;
                _movePhaseTime = 0f;
            }
            else if (_isMovePhase)
            {
                OnMove?.Invoke(_desiredPosition);
                _movePhaseTime += delta;
                if (Vector2.Distance(transform.position, _desiredPosition) < 0.1f || _movePhaseTime >= _maxMovePhaseTime)
                {
                    _isMovePhase = false;
                    OnAttack?.Invoke();
                    OnMove?.Invoke(Vector2.zero);
                }
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

        public void OnAttackEnd()
        {
            _isAttackPhase = false;
        }
    }
}