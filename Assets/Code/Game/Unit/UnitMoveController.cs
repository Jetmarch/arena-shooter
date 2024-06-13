using ArenaShooter.Components;
using ArenaShooter.Inputs;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    /// <summary>
    /// ��������� ����������� ����� � ������� BaseInputController
    /// TODO: ���������� ��� GRASP Controller
    /// </summary>

    [RequireComponent(typeof(Move2DComponent))]
    [RequireComponent(typeof(UnitConditionContainer))]
    public sealed class UnitMoveController : MonoBehaviour
    {
        private UnitConditionContainer _conditionContainer;
        private IMoveInputProvider _inputController;
        private Move2DComponent _moveComponent;

        public void Constuct(IMoveInputProvider inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            //TODO: �������������� � ������� Zenject
            _conditionContainer = GetComponent<UnitConditionContainer>();
            _moveComponent = GetComponent<Move2DComponent>();
        }

        private void OnEnable()
        {
            _inputController.OnMove += OnMove;
        }

        private void OnDisable()
        {
            _inputController.OnMove -= OnMove;
        }

        public void OnMove(Vector2 moveVector)
        {
            if (_conditionContainer.IsDashing) return;

            _moveComponent.Move(moveVector, _conditionContainer.BaseSpeed + _conditionContainer.AdditionalSpeed);
        }
    }
}