using System;
using UnityEngine;

namespace ArenaShooter.Inputs
{
    public abstract class BaseInputController : MonoBehaviour
    {
        public event Action<Vector2> Move;
        public event Action<Vector3> MouseMove;
        public event Action Dash;
        public event Action ChangeWeaponUp;
        public event Action ChangeWeaponDown;
        public event Action Shoot;
        public event Action Reload;

        public abstract Vector3 GetMousePos();
        public abstract Vector2 GetMoveVector();
        protected abstract bool IsChangeWeaponDown();
        protected abstract bool IsChangeWeaponUp();
        protected abstract bool IsDash();
        protected abstract bool IsReload();
        protected abstract bool IsShoot();

        protected virtual void FixedUpdate()
        {
            Move?.Invoke(GetMoveVector());
           
        }

        protected virtual void Update()
        {
            MouseMove?.Invoke(GetMousePos());

            if (IsChangeWeaponDown())
            {
                ChangeWeaponDown?.Invoke();
            }

            if (IsChangeWeaponUp())
            {
                ChangeWeaponUp?.Invoke();
            }

            if (IsDash())
            {
                Dash?.Invoke();
            }

            if (IsReload())
            {
                Reload?.Invoke();
            }

            if (IsShoot())
            {
                Shoot?.Invoke();
            }
        }
    }
}