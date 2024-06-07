using ArenaShooter.Inputs;
using System.Collections;
using UnityEngine;


namespace ArenaShooter.Units
{
    /// <summary>
    /// Позволяет делать рывок
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class UnitDashController : MonoBehaviour
    {
        private UnitConditionContainer _conditionContainer;
        private UnitMoveController _moveController;
        private BaseInputController _inputController;

        private void Start()
        {
            _inputController = GetComponent<BaseInputController>();
            _conditionContainer = GetComponent<UnitConditionContainer>();
            _moveController = GetComponent<UnitMoveController>();

            _inputController.Dash += OnDash;
        }

        private void OnEnable()
        {
            if (_inputController == null) return;

            _inputController.Dash += OnDash;
        }
        private void OnDisable()
        {
            _inputController.Dash -= OnDash;
        }

        public void OnDash()
        {
            if (_conditionContainer.IsDashing) return;

            StartCoroutine(Dashing());
        }

        private IEnumerator Dashing()
        {
            _conditionContainer.IsDashing = true;
            _conditionContainer.AdditionalSpeed += _conditionContainer.DashSpeed;
            yield return new WaitForSeconds(_conditionContainer.DashTime);
            _conditionContainer.AdditionalSpeed -= _conditionContainer.DashSpeed;
            _conditionContainer.IsDashing = false;
        }
    }
}