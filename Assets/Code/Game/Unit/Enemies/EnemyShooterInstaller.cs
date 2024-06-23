using ArenaShooter.AI;
using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Inputs;
using ArenaShooter.Units.Player;
using ArenaShooter.Weapons;
using ArenaShooter.Weapons.Projectiles;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(Move2DComponent))]
    [RequireComponent(typeof(AIInputController))]
    [RequireComponent(typeof(UnitDieMechanic))]
    public class EnemyShooterInstaller : MonoInstaller
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Move2DComponent _moveComponent;
        [SerializeField]
        private AIInputController _inputController;
        [SerializeField]
        private AIBrain _brain;

        [SerializeField]
        private CircleTrigger2DComponent _triggerComponent;
        [SerializeField]
        private PlayerScannerComponent _playerScanner;
        [SerializeField]
        private BaseWeaponInstaller _weaponInstaller;

        [Inject]
        private ProjectileFactory _projectileFactory;

        public override void InstallBindings()
        {
            _moveComponent.Construct(_rigidbody);
            _brain.Construct(_inputController, _playerScanner);
            _weaponInstaller.Construct(_inputController, _inputController, _inputController, _inputController, _projectileFactory);
            _triggerComponent.Construct();
            _playerScanner.Construct(_triggerComponent);

            Container.Bind<UnitDieMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<HealthComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<AIInputController>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<UnitMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDieController>().AsSingle().NonLazy();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _moveComponent = GetComponent<Move2DComponent>();
            _inputController = GetComponent<AIInputController>();
            _brain = GetComponent<AIBrain>();
            _triggerComponent = GetComponentInChildren<CircleTrigger2DComponent>();
            _playerScanner = GetComponentInChildren<PlayerScannerComponent>();
            _weaponInstaller = GetComponentInChildren<BaseWeaponInstaller>();
        }
#endif
    }
}