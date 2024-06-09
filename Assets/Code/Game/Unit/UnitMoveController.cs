using ArenaShooter.Components;
using ArenaShooter.Inputs;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    /// <summary>
    /// Позволяет передвигать юнита с помощью BaseInputController
    /// </summary>

    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(UnitConditionContainer))]
    public sealed class UnitMoveController : MonoBehaviour
    {
        private UnitConditionContainer _conditionContainer;
        private BaseInputController _inputController;
        private MoveComponent _moveComponent;

        [Inject]
        private void Constuct(BaseInputController inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            //TODO: Конструировать с помощью Zenject
            _conditionContainer = GetComponent<UnitConditionContainer>();
            _moveComponent = GetComponent<MoveComponent>();
        }

        private void OnEnable()
        {
            _inputController.Move += OnMove;
        }

        private void OnDisable()
        {
            if (_inputController == null) return;
            _inputController.Move -= OnMove;
        }

        public void OnMove(Vector2 moveVector)
        {
            if (_conditionContainer.IsDashing) return;

            _moveComponent.OnMoveFixedUpdate(moveVector, _conditionContainer.BaseSpeed + _conditionContainer.AdditionalSpeed);
        }
    }
}