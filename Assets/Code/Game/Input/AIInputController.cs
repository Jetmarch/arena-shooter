using System;
using UnityEngine;

namespace ArenaShooter.Inputs
{

    public class AIInputController : MonoBehaviour, IShootInputProvider, IMoveInputProvider
    {
        public event Action OnShoot;
        public event Action<Vector2> OnMove;

        public void Move(Vector2 value)
        {
            OnMove?.Invoke(value);
        }

        public void Shoot()
        {
            OnShoot?.Invoke();
        }
    }
}