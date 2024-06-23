using ArenaShooter.Components;
using ArenaShooter.Inputs;
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

        [Inject]
        private PlayerWeaponFactory _weaponFactory;

        public override void InstallBindings()
        {
            _moveComponent.Construct(_rigidbody2D);
            _weaponChangeMechanic.Construct(_weaponStorage);
            _dashMechanic.Construct(_moveComponent);

            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<WeaponsStorage>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<WeaponChangeMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<HealthComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<UnitDashMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<UnitDieMechanic>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<UnitMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitWeaponChangeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDashController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDieController>().AsSingle().NonLazy();

            _moveComponent.Condition.Append(_dashMechanic.IsNotDashing);
        }

        public override void Start()
        {
            //For test
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.Revolver, _weaponListParent.position, _weaponListParent));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.Shotgun, _weaponListParent.position, _weaponListParent));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.MachineGun, _weaponListParent.position, _weaponListParent));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.RocketLauncher, _weaponListParent.position, _weaponListParent));
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _moveComponent = GetComponent<Move2DComponent>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _weaponChangeMechanic = GetComponent<WeaponChangeMechanic>();
            _weaponStorage = GetComponent<WeaponsStorage>();
            _dashMechanic = GetComponent<UnitDashMechanic>();
        }
#endif
    }
}