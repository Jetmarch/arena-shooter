using System;
using UnityEngine;

namespace ArenaShooter.Inputs
{

    public class AIInputController : MonoBehaviour,
        IShootInputProvider, IMoveInputProvider,
        IMouseMoveInputProvider, IReloadInputProvider
    {
        public event Action OnShoot;
        public event Action<Vector2> OnMove;
        public event Action<Vector3> OnMouseMove;
        public event Action OnReload;

        public void Move(Vector2 value)
        {
            OnMove?.Invoke(value);
        }

        public void Shoot()
        {
            OnShoot?.Invoke();
        }

        public void MouseMove(Vector3 mousePos)
        {
            OnMouseMove?.Invoke(mousePos);
        }

        public void Reload()
        {
            OnReload?.Invoke();
        }
    }
}