using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Move2DComponent : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void OnMoveFixedUpdate(Vector2 moveVector, float speed)
        {
            _rigidbody.velocity = moveVector * Time.fixedDeltaTime * speed;
        }
    }
}