using UnityEngine;

namespace ArenaShooter.Inputs
{

    public class AIInputController : BaseInputController
    {
        private Vector2 _moveVector;
        public void SetMoveVector(Vector2 value)
        {
            _moveVector = value;
        }

        public override Vector3 GetMousePos()
        {
            return Vector3.zero;
        }

        public override Vector2 GetMoveVector()
        {
            return _moveVector;
        }

        protected override bool IsChangeWeaponDown()
        {
            return false;
        }

        protected override bool IsChangeWeaponUp()
        {
            return false;
        }

        protected override bool IsDash()
        {
            return false;
        }

        protected override bool IsReload()
        {
            return false;
        }

        protected override bool IsShoot()
        {
            return false;
        }
    }
}