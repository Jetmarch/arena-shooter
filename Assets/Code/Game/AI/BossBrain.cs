using ArenaShooter.Inputs;
using System;
using Unity.VisualScripting;
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

        private AIInputController _controller;

        public void Construct(AIInputController controller)
        {
            _controller = controller;
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
            if (_target == null || _isAttackPhase)
            {
                _controller.Move(Vector2.zero);
                return;
            }
            _controller.ScreenMouseMove(_target.position);
            _controller.WorldMouseMove(_target.position);

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
                _controller.Move(_desiredPosition);
                _movePhaseTime += delta;
                if (Vector2.Distance(transform.position, _desiredPosition) < 0.1f || _movePhaseTime >= _maxMovePhaseTime)
                {
                    _isMovePhase = false;
                    _controller.Shoot();
                    _controller.Move(Vector2.zero);
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