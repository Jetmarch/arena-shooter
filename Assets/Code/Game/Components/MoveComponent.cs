using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveComponent : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void OnMoveFixedUpdate(Vector2 moveVector, float speed)
        {
            _rigidbody.velocity = moveVector * Time.fixedDeltaTime * speed;
        }
    }
}