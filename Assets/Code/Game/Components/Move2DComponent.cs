using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Move2DComponent : MonoBehaviour
    {
        private float _moveSpeed;

        private Rigidbody2D _rigidbody;

        private Vector2 _velocity;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void OnMoveFixedUpdate(Vector2 moveVector, float speed)
        {
            _velocity = moveVector;
            _moveSpeed = speed;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _velocity * Time.fixedDeltaTime * _moveSpeed;
        }
    }
}