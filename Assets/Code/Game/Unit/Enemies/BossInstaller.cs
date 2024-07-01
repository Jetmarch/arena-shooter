using ArenaShooter.AI;
using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Enemies
{
    [RequireComponent(typeof(Move2DComponent))]
    [RequireComponent(typeof(BossBrain))]
    [RequireComponent(typeof(WeaponsStorage))]
    [RequireComponent(typeof(WeaponChangeMechanic))]
    [RequireComponent(typeof(UnitDieMechanic))]
    [RequireComponent(typeof(HealthComponent))]
    public class BossInstaller : MonoInstaller
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private Move2DComponent _moveComponent;

        [SerializeField]
        private CircleTrigger2DComponent _circleTrigger;

        [SerializeField]
        private PlayerScannerComponent _playerScanner;

        [SerializeField]
        private BossBrain _bossBrain;

        [SerializeField]
        private WeaponsStorage _weaponStorage;

        [SerializeField]
        private WeaponChangeMechanic _weaponChangeMechanic;

        [SerializeField]
        private BossAttackPattern _bossAttackPattern;

        [SerializeField]
        private HealthComponent _healthComponent;

        [SerializeField]
        private UnitDieMechanic _unitDieMechanic;

        [SerializeField]
        private SpriteFlashMechanic _spriteFlashMechanic;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private UnitTemporaryInvulnerableMechanic _unitTemporaryInvulnerableMechanic;


        public override void InstallBindings()
        {
            //TODO: Избавиться от лишних вызовов, собирая компоненты внутри интерфейса
            _healthComponent.Construct();
            _moveComponent.Construct(_rigidbody);
            _weaponChangeMechanic.Construct(_weaponStorage);
            _circleTrigger.Construct();
            _playerScanner.Construct(_circleTrigger);
            _spriteFlashMechanic.Construct(_spriteRenderer);

            Container.Bind<BossBrain>().FromInstance(_bossBrain).AsSingle();
            Container.Bind<Move2DComponent>().FromInstance(_moveComponent).AsSingle();
            Container.Bind<PlayerScannerComponent>().FromInstance(_playerScanner).AsSingle();
            
            Container.Bind<HealthComponent>().FromInstance(_healthComponent).AsSingle();
            Container.Bind<UnitDieMechanic>().FromInstance(_unitDieMechanic).AsSingle();
            Container.Bind<WeaponChangeMechanic>().FromInstance(_weaponChangeMechanic).AsSingle();
            Container.Bind<SpriteFlashMechanic>().FromInstance(_spriteFlashMechanic).AsSingle();
            Container.Bind<UnitTemporaryInvulnerableMechanic>().FromInstance(_unitTemporaryInvulnerableMechanic).AsSingle();

            Container.BindInterfacesAndSelfTo<BossAttackPattern>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BossPlayerScannerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BossAttackPatternController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BossMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDieController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpriteFlashOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TemporaryInvulnerabilityOnHitController>().AsSingle().NonLazy();

            _healthComponent.Condition.Append(_unitTemporaryInvulnerableMechanic.IsNotInvulnerable);

            SetWeaponsOwner();
        }

        private void SetWeaponsOwner()
        {
            foreach(var weapon in _weaponStorage.Weapons)
            {
                weapon.WeaponShootMechanic.SetOwner(gameObject);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _moveComponent = GetComponent<Move2DComponent>();
            _circleTrigger = GetComponentInChildren<CircleTrigger2DComponent>();
            _playerScanner = GetComponentInChildren<PlayerScannerComponent>();
            _weaponStorage = GetComponent<WeaponsStorage>();
            _weaponChangeMechanic = GetComponent<WeaponChangeMechanic>();
            _bossBrain = GetComponent<BossBrain>();
            _healthComponent = GetComponent<HealthComponent>();
            _unitDieMechanic = GetComponent<UnitDieMechanic>();
            _spriteFlashMechanic = GetComponentInChildren<SpriteFlashMechanic>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _unitTemporaryInvulnerableMechanic = GetComponent<UnitTemporaryInvulnerableMechanic>();
        }
#endif
    }
}