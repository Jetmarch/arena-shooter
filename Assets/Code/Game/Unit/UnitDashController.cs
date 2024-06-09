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
    [RequireComponent(typeof(MoveComponent))]
    [RequireComponent(typeof(UnitConditionContainer))]
    public sealed class UnitDashController : MonoBehaviour
    {
        private UnitConditionContainer _conditionContainer;
        private MoveComponent _moveComponent;
        private BaseInputController _inputController;

        [SerializeField]
        private Vector2 _dashVector;

        [Inject]
        private void Construct(BaseInputController inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            //_inputController = GetComponent<BaseInputController>();
            _conditionContainer = GetComponent<UnitConditionContainer>();
            _moveComponent = GetComponent<MoveComponent>();

            //_inputController.Dash += OnDash;
        }

        private void OnEnable()
        {
            _inputController.Dash += OnDash;
        }
        private void OnDisable()
        {
            if (_inputController == null) return;

            _inputController.Dash -= OnDash;
        }

        private void FixedUpdate()
        {
            if (!_conditionContainer.IsDashing) return;

            _moveComponent.OnMoveFixedUpdate(_dashVector, _conditionContainer.DashSpeed);
        }

        public void OnDash()
        {
            if (_conditionContainer.IsDashing) return;

            StartCoroutine(Dashing());
            _dashVector = _inputController.GetMoveVector().normalized;
        }

        private IEnumerator Dashing()
        {
            _conditionContainer.IsDashing = true;
            _conditionContainer.AdditionalSpeed += _conditionContainer.DashSpeed;

            yield return new WaitForSeconds(_conditionContainer.DashTime);

            _conditionContainer.AdditionalSpeed -= _conditionContainer.DashSpeed;
            _conditionContainer.IsDashing = false;
            _dashVector = Vector2.zero;
        }
    }
}