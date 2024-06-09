using System;
using UnityEngine;

namespace ArenaShooter.Inputs
{
    public sealed class KeyboardAndMouseInputController : BaseInputController
    {
        [SerializeField]
        private KeyCode _moveUpKey = KeyCode.W;
        [SerializeField]
        private KeyCode _moveDownKey = KeyCode.S;
        [SerializeField]
        private KeyCode _moveRightKey = KeyCode.D;
        [SerializeField]
        private KeyCode _moveLeftKey = KeyCode.A;

        [SerializeField]
        private KeyCode _dashKey = KeyCode.LeftShift;
        [SerializeField]
        private KeyCode _reloadKey = KeyCode.R;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void Update()
        {
            base.Update();
        }

        public override Vector3 GetMousePos()
        {
            return Input.mousePosition;
        }

        public override Vector2 GetMoveVector()
        {
            Vector2 moveVector = Vector2.zero;
            if (Input.GetKey(_moveUpKey))
            {
                moveVector += Vector2.up;
            }
            if (Input.GetKey(_moveDownKey))
            {
                moveVector += Vector2.down;
            }
            if (Input.GetKey(_moveRightKey))
            {
                moveVector += Vector2.right;
            }
            if (Input.GetKey(_moveLeftKey))
            {
                moveVector += Vector2.left;
            }
            return moveVector;
        }

        protected override bool IsChangeWeaponDown()
        {
            if (Input.mouseScrollDelta.y < 0)
            {
                return true;
            }

            return false;
        }

        protected override bool IsChangeWeaponUp()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                return true;
            }

            return false;
        }

        protected override bool IsDash()
        {
            if (Input.GetKeyDown(_dashKey))
            {
                return true;
            }
            return false;
        }

        protected override bool IsReload()
        {
            if (Input.GetKeyDown(_reloadKey))
            {
                return true;
            }

            return false;
        }

        protected override bool IsShoot()
        {
            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }
            return false;
        }
    }
}