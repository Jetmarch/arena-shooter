using ArenaShooter.Components;
using ArenaShooter.Units.Enemies;
using ArenaShooter.Weapons;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Move2DComponent))]
    [RequireComponent(typeof(UnitDashMechanic))]
    [RequireComponent(typeof(WeaponChangeMechanic))]
    [RequireComponent(typeof(WeaponsStorage))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(UnitTemporaryInvulnerableMechanic))]
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private Move2DComponent _moveComponent;
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        [SerializeField]
        private WeaponChangeMechanic _weaponChangeMechanic;
        [SerializeField]
        private WeaponsStorage _weaponStorage;
        [SerializeField]
        private UnitDashMechanic _dashMechanic;

        [SerializeField]
        private Transform _weaponListParent;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private SpriteFlashMechanic _flashMechanic;

        [SerializeField]
        private HealthComponent _healthComponent;
        [SerializeField]
        private UnitTemporaryInvulnerableMechanic _unitTemporaryInvulnerableMechanic;

        [Inject]
        private PlayerWeaponFactory _weaponFactory;

        public override void InstallBindings()
        {
            _healthComponent.Construct();
            _moveComponent.Construct(_rigidbody2D);
            _weaponChangeMechanic.Construct(_weaponStorage);
            _dashMechanic.Construct(_moveComponent);
            _flashMechanic.Construct(_spriteRenderer);

            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<HealthComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<UnitDieMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<UnitTemporaryInvulnerableMechanic>().FromComponentOn(gameObject).AsSingle();

            Container.Bind<WeaponsStorage>().FromInstance(_weaponStorage).AsSingle();
            Container.Bind<WeaponChangeMechanic>().FromInstance(_weaponChangeMechanic).AsSingle();
            Container.Bind<UnitDashMechanic>().FromInstance(_dashMechanic).AsSingle();
            Container.Bind<SpriteRenderer>().FromInstance(_spriteRenderer).AsSingle();
            Container.Bind<SpriteFlashMechanic>().FromInstance(_flashMechanic).AsSingle();

            Container.BindInterfacesAndSelfTo<UnitMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitWeaponChangeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDashController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDieController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpriteFlashOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TemporaryInvulnerabilityOnHitController>().AsSingle().NonLazy();

            _moveComponent.Condition.Append(_dashMechanic.IsNotDashing);
            _healthComponent.Condition.Append(_unitTemporaryInvulnerableMechanic.IsNotInvulnerable);

        }

        private void SetWeaponsOwner()
        {
            foreach (var weapon in _weaponStorage.Weapons)
            {
                weapon.WeaponShootMechanic.SetOwner(gameObject);
            }
        }

        public override void Start()
        {
            //For test
            //TODO: Переместить в PlayerWeaponGiver
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.Revolver, _weaponListParent.position, _weaponListParent));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.Shotgun, _weaponListParent.position, _weaponListParent));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.MachineGun, _weaponListParent.position, _weaponListParent));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.RocketLauncher, _weaponListParent.position, _weaponListParent));
            _weaponChangeMechanic.OnChangeWeaponUp();
            SetWeaponsOwner();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _moveComponent = GetComponent<Move2DComponent>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _weaponChangeMechanic = GetComponent<WeaponChangeMechanic>();
            _weaponStorage = GetComponent<WeaponsStorage>();
            _dashMechanic = GetComponent<UnitDashMechanic>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _flashMechanic = GetComponentInChildren<SpriteFlashMechanic>();
            _unitTemporaryInvulnerableMechanic = GetComponent<UnitTemporaryInvulnerableMechanic>();
            _healthComponent = GetComponentInChildren<HealthComponent>();
        }
#endif
    }
}