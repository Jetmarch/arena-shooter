using ArenaShooter.Inputs;
using System.Collections;
using UnityEngine;

namespace ArenaShooter.AI
{
    public class BossBrain : MonoBehaviour, IGameUpdateListener, IGamePauseListener, IAIBrain
    {
        [SerializeField]
        private float _radiusOfInteraction = 3f;
        [SerializeField]
        private float _maxMovePhaseTime = 3f;
        [SerializeField]
        protected float _maxStunTime = 2f;

        private bool _isAttackPhase = false;
        private bool _isMovePhase = false;

        private Vector3 _desiredPosition;
        private float _movePhaseTime;

        private Transform _target;

        private AIInputController _inputController;

        private bool _isPaused;

        public void Construct(AIInputController controller)
        {
            _inputController = controller;
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
            if (_isPaused) return;

            if (_target == null || _isAttackPhase)
            {
                _inputController.Move(Vector2.zero);
                return;
            }
            _inputController.ScreenMouseMove(_target.position);
            _inputController.WorldMouseMove(_target.position);

            if (!_isMovePhase)
            {
                var rndInCirclePosition = Random.insideUnitCircle * _radiusOfInteraction;
                var positionNearTarget = new Vector3(_target.position.x + rndInCirclePosition.x, _target.position.y + rndInCirclePosition.y, 0f);
                _desiredPosition = (positionNearTarget - transform.position).normalized;

                _isMovePhase = true;
                _movePhaseTime = 0f;
            }
            else if (_isMovePhase)
            {
                _inputController.Move(_desiredPosition);
                _movePhaseTime += delta;
                if (Vector2.Distance(transform.position, _desiredPosition) < 0.1f || _movePhaseTime >= _maxMovePhaseTime)
                {
                    _isMovePhase = false;
                    _inputController.Shoot();
                    _inputController.Move(Vector2.zero);
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

        public void OnPauseGame()
        {
            StopBrain();
        }

        public void OnResumeGame()
        {
            StartBrain();
        }

        public void StartBrain()
        {
            _isPaused = false;
        }

        public void StopBrain()
        {
            _isPaused = true;
            _inputController.Move(Vector2.zero);
        }

        public void Stun()
        {
            StartCoroutine(Stunned());
        }

        private IEnumerator Stunned()
        {
            StopBrain();
            yield return new WaitForSeconds(_maxStunTime);
            StartBrain();
        }
    }
}