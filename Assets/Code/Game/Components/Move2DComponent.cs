using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Move2DComponent : MonoBehaviour, IGameFixedUpdateListener
    {
        private float _moveSpeed;

        private Rigidbody2D _rigidbody;

        private Vector2 _velocity;

        public Vector2 Velocity { get { return _velocity; } }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }

        public void Move(Vector2 moveVector, float speed)
        {
            _velocity = moveVector;
            _moveSpeed = speed;
        }

        public void OnFixedUpdate(float delta)
        {
            _rigidbody.velocity = _velocity * Time.fixedDeltaTime * _moveSpeed;
        }
    }
}