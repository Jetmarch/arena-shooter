using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Inputs
{
    public sealed class KeyboardAndMouseInputController : MonoBehaviour,
        IMoveInputProvider, IScreenMouseMoveInputProvider, IWorldMouseMoveInputProvider,
        IShootInputProvider, IReloadInputProvider, IChangeWeaponInputProvider, IDashInputProvider,
        IGameUpdateListener, IInteractInputProvider
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
        [SerializeField]
        private KeyCode _interact = KeyCode.E;

        public event Action<Vector2> OnMove;
        public event Action<Vector3> OnScreenMouseMove;
        public event Action OnShoot;
        public event Action OnShootHold;
        public event Action OnReload;
        public event Action OnChangeWeaponUp;
        public event Action OnChangeWeaponDown;
        public event Action OnDash;
        public event Action<Vector3> OnWorldMouseMove;
        public event Action OnInteract;

        private Camera _camera;

        [Inject]
        private void Construct(Camera camera)
        {
            _camera = camera;
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }

        public void OnUpdate(float delta)
        {
            ScreenMouseMove();
            WorldMouseMove();
            Move();
            ChangeWeaponDown();
            ChangeWeaponUp();
            Dash();
            Reload();
            Shoot();
            ShootHold();
            Interact();
        }

        private void ScreenMouseMove()
        {
            OnScreenMouseMove?.Invoke(Input.mousePosition);
        }

        private void WorldMouseMove()
        {
            OnWorldMouseMove?.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
        }

        private void Move()
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
            OnMove?.Invoke(moveVector);
        }

        private void ChangeWeaponDown()
        {
            if (Input.mouseScrollDelta.y < 0)
            {
                OnChangeWeaponDown?.Invoke();
            }
        }

        private void ChangeWeaponUp()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                OnChangeWeaponUp?.Invoke();
            }
        }

        private void Dash()
        {
            if (Input.GetKeyDown(_dashKey))
            {
                OnDash?.Invoke();
            }
        }

        private void Reload()
        {
            if (Input.GetKeyDown(_reloadKey))
            {
                OnReload?.Invoke();
            }
        }

        private void Shoot()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnShoot?.Invoke();
            }
        }

        private void ShootHold()
        {
            if (Input.GetMouseButton(0))
            {
                OnShootHold?.Invoke();
            }
        }

        private void Interact()
        {
            if(Input.GetKeyDown(_interact))
            {
                OnInteract?.Invoke();
            }
        }

    }
}