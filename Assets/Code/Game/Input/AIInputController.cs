using System;
using UnityEngine;

namespace ArenaShooter.Inputs
{

    public class AIInputController : MonoBehaviour,
        IShootInputProvider, IMoveInputProvider,
        IWorldMouseMoveInputProvider, IScreenMouseMoveInputProvider,
        IReloadInputProvider
    {
        public event Action OnShoot;
        public event Action OnShootHold;
        public event Action<Vector2> OnMove;
        public event Action OnReload;
        public event Action<Vector3> OnWorldMouseMove;
        public event Action<Vector3> OnScreenMouseMove;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void Move(Vector2 value)
        {
            OnMove?.Invoke(value);
        }

        public void Shoot()
        {
            OnShoot?.Invoke();
        }

        public void WorldMouseMove(Vector3 mousePos)
        {
            OnWorldMouseMove?.Invoke(mousePos);
        }

        public void ScreenMouseMove(Vector3 mousePos)
        {
            OnScreenMouseMove?.Invoke(_camera.WorldToScreenPoint(mousePos));
        }

        public void Reload()
        {
            OnReload?.Invoke();
        }
    }
}