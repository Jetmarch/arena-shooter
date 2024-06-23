using ArenaShooter.Utils;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Move2DComponent : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField]
        private float _moveSpeed = 250f;

        private Rigidbody2D _rigidbody;

        private CompositeCondition _condition = new();

        private Vector2 _velocity;
        public Vector2 Velocity { get { return _velocity; } }
        public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
        public CompositeCondition Condition { get { return _condition; } }

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
            _rigidbody.velocity = _velocity * delta * _moveSpeed;
        }
    }
}