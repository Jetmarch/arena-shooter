using ArenaShooter.Components;
using ArenaShooter.Inputs;
using System.Collections;
using UnityEngine;
using Zenject;


namespace ArenaShooter.Units
{
    /// <summary>
    /// ��������� ������ �����
    /// </summary>
    [RequireComponent(typeof(Move2DComponent))]
    [RequireComponent(typeof(UnitConditionContainer))]
    public sealed class UnitDashMechanic : MonoBehaviour, IGameFixedUpdateListener
    {
        private UnitConditionContainer _conditionContainer;
        private Move2DComponent _moveComponent;
        private IDashInputProvider _inputController;

        [SerializeField]
        private Vector2 _dashVector;

        [Inject]
        private void Construct(IDashInputProvider inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _conditionContainer = GetComponent<UnitConditionContainer>();
            _moveComponent = GetComponent<Move2DComponent>();
        }

        private void OnEnable()
        {
            _inputController.OnDash += OnDash;
            IGameLoopListener.Register(this);
        }
        private void OnDisable()
        {
            _inputController.OnDash -= OnDash;
            IGameLoopListener.Unregister(this);
        }

        public void OnDash()
        {
            if (_conditionContainer.IsDashing) return;

            StartCoroutine(Dashing());
            _dashVector = _moveComponent.Velocity;
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

        public void OnFixedUpdate(float delta)
        {
            //TODO: ���������� ������� ����� CompositeCondition
            if (!_conditionContainer.IsDashing) return;

            _moveComponent.Move(_dashVector, _conditionContainer.DashSpeed);
        }
    }
}