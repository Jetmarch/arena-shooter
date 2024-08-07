using ArenaShooter.Utils;
using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Move2DComponent : MonoBehaviour, IGameFixedUpdateListener, IGamePauseListener
    {
        [SerializeField]
        private float _moveSpeed = 250f;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        private CompositeCondition _condition = new();

        private Vector2 _velocity;
        public Vector2 Velocity { get { return _velocity; } }
        public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
        public CompositeCondition Condition { get { return _condition; } }

        private bool _isPaused;
        private Vector2 _velocityBeforePause;

        public void Construct(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }

        public void Move(Vector2 moveVector)
        {
            _velocity = moveVector;
        }

        public void OnFixedUpdate(float delta)
        {
            if (_isPaused) return;

            _rigidbody.velocity = _velocity * delta * _moveSpeed;
        }

        public void OnPauseGame()
        {
            _isPaused = true;
            _velocityBeforePause = _rigidbody.velocity;
            _rigidbody.velocity = Vector2.zero;
        }

        public void OnResumeGame()
        {
            _isPaused = false;
            _rigidbody.velocity = _rigidbody.velocity;
        }
    }
}