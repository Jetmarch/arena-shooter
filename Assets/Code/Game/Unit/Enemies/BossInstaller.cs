using ArenaShooter.AI;
using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Weapons;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Enemies
{
    [RequireComponent(typeof(Move2DComponent))]
    [RequireComponent(typeof(BossBrain))]
    [RequireComponent(typeof(WeaponsStorage))]
    [RequireComponent(typeof(WeaponChangeMechanic))]
    [RequireComponent(typeof(BossAttackPattern))]
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


        public override void InstallBindings()
        {
            _moveComponent.Construct(_rigidbody);
            _weaponChangeMechanic.Construct(_weaponStorage);
            _bossAttackPattern.Construct(_weaponChangeMechanic);
            _circleTrigger.Construct();
            _playerScanner.Construct(_circleTrigger);

            Container.Bind<BossBrain>().FromInstance(_bossBrain).AsSingle();
            Container.Bind<Move2DComponent>().FromInstance(_moveComponent).AsSingle();
            Container.Bind<PlayerScannerComponent>().FromInstance(_playerScanner).AsSingle();
            Container.Bind<BossAttackPattern>().FromInstance(_bossAttackPattern).AsSingle();
            Container.Bind<HealthComponent>().FromInstance(_healthComponent).AsSingle();
            Container.Bind<UnitDieMechanic>().FromInstance(_unitDieMechanic).AsSingle();

            Container.BindInterfacesAndSelfTo<BossPlayerScannerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BossAttackPatternController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BossMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDieController>().AsSingle().NonLazy();
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
            _bossAttackPattern = GetComponent<BossAttackPattern>();
            _bossBrain = GetComponent<BossBrain>();
            _healthComponent = GetComponent<HealthComponent>();
            _unitDieMechanic = GetComponent<UnitDieMechanic>();
        }
#endif
    }
}