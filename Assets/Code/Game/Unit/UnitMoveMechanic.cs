using ArenaShooter.Components;
using ArenaShooter.Inputs;
using ArenaShooter.Utils;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    /// <summary>
    /// Позволяет передвигать юнита
    /// </summary>

    [RequireComponent(typeof(Move2DComponent))]
    public sealed class UnitMoveMechanic : MonoBehaviour
    {
        private IMoveInputProvider _inputController;
        private Move2DComponent _moveComponent;

        private CompositeCondition _condition;

        public CompositeCondition Condition { get { return _condition; } }

        public void Constuct(IMoveInputProvider inputController, Move2DComponent moveComponent)
        {
            _inputController = inputController;
            _inputController.OnMove += OnMove;

            _moveComponent = moveComponent;
            _condition = new CompositeCondition();
        }

        private void OnEnable()
        {
            if (_inputController == null) return;
            _inputController.OnMove += OnMove;
        }

        private void OnDisable()
        {
            if (_inputController == null) return;
            _inputController.OnMove -= OnMove;
        }

        public void OnMove(Vector2 moveVector)
        {
            if (!_condition.IsTrue()) return;

            _moveComponent.Move(moveVector);
        }
    }
}