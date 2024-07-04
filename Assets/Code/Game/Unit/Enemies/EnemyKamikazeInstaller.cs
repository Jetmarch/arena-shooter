using ArenaShooter.AI;
using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Inputs;
using ArenaShooter.Units.Player;
using ArenaShooter.Weapons;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Enemies
{
    public class EnemyKamikazeInstaller : MonoInstaller
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
        private BaseWeaponShootMechanic _weaponShootMechanic;
        [SerializeField]
        private SpriteFlashMechanic _spriteFlashMechanic;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private HealthComponent _healthComponent;
        [SerializeField]
        private UnitTemporaryInvulnerableMechanic _unitTemporaryInvulnerableMechanic;

        public override void InstallBindings()
        {
            _healthComponent.Construct();
            _moveComponent.Construct(_rigidbody);
            _brain.Construct(_inputController, _playerScanner);
            _triggerComponent.Construct();
            _playerScanner.Construct(_triggerComponent);
            _spriteFlashMechanic.Construct(_spriteRenderer);

            Container.Bind<UnitDieMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<HealthComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<UnitTemporaryInvulnerableMechanic>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<AIInputController>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<SpriteFlashMechanic>().FromInstance(_spriteFlashMechanic).AsSingle();
            Container.Bind<SpriteRenderer>().FromInstance(_spriteRenderer).AsSingle();


            Container.BindInterfacesAndSelfTo<UnitMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDieController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpriteFlashOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TemporaryInvulnerabilityOnHitController>().AsSingle().NonLazy();

            _healthComponent.Condition.Append(_unitTemporaryInvulnerableMechanic.IsNotInvulnerable);

            _inputController.OnShoot += _weaponShootMechanic.Shoot;
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
            _weaponShootMechanic = GetComponentInChildren<BaseWeaponShootMechanic>();
            _spriteFlashMechanic = GetComponentInChildren<SpriteFlashMechanic>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _unitTemporaryInvulnerableMechanic = GetComponent<UnitTemporaryInvulnerableMechanic>();
            _healthComponent = GetComponentInChildren<HealthComponent>();
        }
#endif
    }
}