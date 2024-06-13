using ArenaShooter.Components;
using ArenaShooter.Inputs;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    /// <summary>
    /// Позволяет передвигать юнита с помощью BaseInputController
    /// TODO: Переделать под GRASP Controller
    /// </summary>

    [RequireComponent(typeof(Move2DComponent))]
    [RequireComponent(typeof(UnitConditionContainer))]
    public sealed class UnitMoveMechanic : MonoBehaviour
    {
        private UnitConditionContainer _conditionContainer;
        private IMoveInputProvider _inputController;
        private Move2DComponent _moveComponent;

        public void Constuct(IMoveInputProvider inputController)
        {
            _inputController = inputController;
            _inputController.OnMove += OnMove;
        }

        private void Start()
        {
            //TODO: Конструировать с помощью Zenject
            _conditionContainer = GetComponent<UnitConditionContainer>();
            _moveComponent = GetComponent<Move2DComponent>();
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
            if (_conditionContainer.IsDashing) return;

            _moveComponent.Move(moveVector, _conditionContainer.BaseSpeed + _conditionContainer.AdditionalSpeed);
        }
    }
}