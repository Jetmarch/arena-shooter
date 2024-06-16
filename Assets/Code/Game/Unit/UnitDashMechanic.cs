using ArenaShooter.Components;
using ArenaShooter.Inputs;
using System.Collections;
using UnityEngine;
using Zenject;


namespace ArenaShooter.Units
{
    /// <summary>
    /// Позволяет делать рывок
    /// </summary>
    [RequireComponent(typeof(Move2DComponent))]
    public sealed class UnitDashMechanic : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField]
        private float _dashSpeed = 500f;

        [SerializeField]
        private float _dashTime = 0.25f;

        [SerializeField]
        private bool _isDashing = false;

        private Move2DComponent _moveComponent;
        private IDashInputProvider _inputController;

        [SerializeField]
        private Vector2 _dashVector;

        public void Construct(IDashInputProvider inputController, Move2DComponent moveComponent)
        {
            _inputController = inputController;
            _moveComponent = moveComponent;
        }

        private void OnEnable()
        {
            if (_inputController == null) return;
            _inputController.OnDash += OnDash;
            IGameLoopListener.Register(this);
        }
        private void OnDisable()
        {
            if (_inputController == null) return;
            _inputController.OnDash -= OnDash;
            IGameLoopListener.Unregister(this);
        }

        public bool IsNotDashing()
        {
            return !_isDashing;
        }

        public void OnDash()
        {
            if (_isDashing) return;

            StartCoroutine(Dashing());
            _dashVector = _moveComponent.Velocity;
        }

        private IEnumerator Dashing()
        {
            _isDashing = true;
            _moveComponent.MoveSpeed += _dashSpeed;

            yield return new WaitForSeconds(_dashTime);

            _moveComponent.MoveSpeed -= _dashSpeed;
            _isDashing = false;
            _dashVector = Vector2.zero;
        }

        public void OnFixedUpdate(float delta)
        {
            if (!_isDashing) return;

            _moveComponent.Move(_dashVector);
        }
    }
}