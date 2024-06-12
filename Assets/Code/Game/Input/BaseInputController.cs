using System;
using UnityEngine;

namespace ArenaShooter.Inputs
{
    public abstract class BaseInputController : MonoBehaviour
    {
        public event Action<Vector2> OnMove;
        public event Action<Vector3> OnMouseMove;
        public event Action OnDash;
        public event Action OnChangeWeaponUp;
        public event Action OnChangeWeaponDown;
        public event Action OnShoot;
        public event Action OnReload;

        public void Shoot()
        {
            OnShoot?.Invoke();
        }

        public abstract Vector3 GetMousePos();
        public abstract Vector2 GetMoveVector();
        protected abstract bool IsChangeWeaponDown();
        protected abstract bool IsChangeWeaponUp();
        protected abstract bool IsDash();
        protected abstract bool IsReload();
        protected abstract bool IsShoot();


        protected virtual void Update()
        {
            OnMove?.Invoke(GetMoveVector());
            OnMouseMove?.Invoke(GetMousePos());

            if (IsChangeWeaponDown())
            {
                OnChangeWeaponDown?.Invoke();
            }

            if (IsChangeWeaponUp())
            {
                OnChangeWeaponUp?.Invoke();
            }

            if (IsDash())
            {
                OnDash?.Invoke();
            }

            if (IsReload())
            {
                OnReload?.Invoke();
            }

            if (IsShoot())
            {
                OnShoot?.Invoke();
            }
        }
    }
}