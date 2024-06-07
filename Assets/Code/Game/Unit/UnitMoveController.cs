using ArenaShooter.Inputs;
using UnityEngine;

namespace ArenaShooter.Units
{
    /// <summary>
    /// Позволяет передвигать юнита c rigidBody
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(UnitConditionContainer))]
    public sealed class UnitMoveController : MonoBehaviour
    {
        private UnitConditionContainer _conditionContainer;
        private Rigidbody _rigidbody;

        private BaseInputController _inputController;

        private void Start()
        {
            //TODO: Конструировать с помощью Zenject
            _inputController = GetComponent<BaseInputController>();
            _conditionContainer = GetComponent<UnitConditionContainer>();
            _rigidbody = GetComponent<Rigidbody>();

            _inputController.Move += OnMove;
        }

        private void OnEnable()
        {
            if (_inputController == null) return;
            _inputController.Move += OnMove;
        }

        private void OnDisable()
        {
            _inputController.Move -= OnMove;
        }

        public void OnMove(Vector2 moveVector)
        {
            _rigidbody.velocity = moveVector * Time.fixedDeltaTime * (_conditionContainer.BaseSpeed + _conditionContainer.AdditionalSpeed);
        }
    }
}