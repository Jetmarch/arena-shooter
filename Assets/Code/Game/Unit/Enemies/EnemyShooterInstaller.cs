using ArenaShooter.AI;
using ArenaShooter.Components;
using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using ArenaShooter.Weapons.Projectiles;
using UnityEngine;

namespace ArenaShooter.Units.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(Move2DComponent))]
    [RequireComponent(typeof(UnitMoveMechanic))]
    [RequireComponent(typeof(AIInputController))]
    [RequireComponent(typeof(UnitDieMechanic))]
    public class EnemyShooterInstaller : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private HealthComponent _healthComponent;
        [SerializeField]
        private Move2DComponent _moveComponent;
        [SerializeField]
        private UnitMoveMechanic _moveController;
        [SerializeField]
        private AIInputController _inputController;
        [SerializeField]
        private UnitDieMechanic _dieMechanic;
        [SerializeField]
        private AIBrain _brain;

        [SerializeField]
        private Trigger2DComponent _triggerComponent;
        [SerializeField]
        private PlayerScannerComponent _playerScanner;
        [SerializeField]
        private WeaponInstaller _weaponInstaller;

        public void Construct(AIStateMachineFactory aiStateMachineFactory, ProjectileFactory projectileFactory)
        {
            _moveComponent.Construct(_rigidbody);
            _moveController.Constuct(_inputController, _moveComponent);
            _dieMechanic.Construct(_healthComponent);
            _brain.Construct(_inputController, aiStateMachineFactory, _playerScanner);
            _weaponInstaller.Construct(_inputController, _inputController, _inputController, projectileFactory);
            _playerScanner.Construct(_triggerComponent);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _healthComponent = GetComponent<HealthComponent>();
            _moveComponent = GetComponent<Move2DComponent>();
            _moveController = GetComponent<UnitMoveMechanic>();
            _inputController = GetComponent<AIInputController>();
            _dieMechanic = GetComponent<UnitDieMechanic>();
            _brain = GetComponent<AIBrain>();
            _triggerComponent = GetComponentInChildren<Trigger2DComponent>();
            _playerScanner = GetComponentInChildren<PlayerScannerComponent>();
            _weaponInstaller = GetComponentInChildren<WeaponInstaller>();
        }
#endif
    }
}