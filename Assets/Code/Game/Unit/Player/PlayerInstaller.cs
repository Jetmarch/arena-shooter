using ArenaShooter.Components;
using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using UnityEngine;

namespace ArenaShooter.Units.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Move2DComponent))]
    [RequireComponent(typeof(UnitMoveMechanic))]
    [RequireComponent(typeof(UnitDashMechanic))]
    [RequireComponent(typeof(WeaponChangeMechanic))]
    [RequireComponent(typeof(WeaponsStorage))]
    [RequireComponent(typeof(UnitDieMechanic))]
    [RequireComponent(typeof(HealthComponent))]
    public class PlayerInstaller : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Move2DComponent _moveComponent;
        [SerializeField]
        private UnitMoveMechanic _moveController;
        [SerializeField]
        private UnitDashMechanic _dashController;
        [SerializeField]
        private WeaponChangeMechanic _weaponChangeMechanic;
        [SerializeField]
        private WeaponsStorage _weaponStorage;
        [SerializeField]
        private HealthComponent _healthComponent;
        [SerializeField]
        private UnitDieMechanic _dieMechanic;

        [SerializeField]
        private Transform _weaponListParent;

        private PlayerWeaponFactory _weaponFactory;

        private IMoveInputProvider _moveInputProvider;
        private IDashInputProvider _dashInputProvider;
        private IChangeWeaponInputProvider _changeWeaponInputProvider;
        //TODO: для анимации поворота персонажа
        private IScreenMouseMoveInputProvider _screenMouseMoveInputProvider;

        public void Construct(IMoveInputProvider moveInputProvider,
                            IDashInputProvider dashInputProvider,
                            IChangeWeaponInputProvider changeWeaponInputProvider,
                            IScreenMouseMoveInputProvider screenMouseMoveInputProvider,
                            PlayerWeaponFactory weaponFactory)
        {
            _changeWeaponInputProvider = changeWeaponInputProvider;
            _moveInputProvider = moveInputProvider;
            _dashInputProvider = dashInputProvider;
            _screenMouseMoveInputProvider = screenMouseMoveInputProvider;
            _weaponFactory = weaponFactory;

            _moveComponent.Construct(_rigidbody);
            _moveController.Constuct(_moveComponent);
            _weaponChangeMechanic.Construct(_weaponStorage);
            _dashController.Construct(_moveComponent);
            _dieMechanic.Construct(_healthComponent);

            _moveController.Condition.Append(_dashController.IsNotDashing);

            _moveInputProvider.OnMove += _moveController.OnMove;
            _dashInputProvider.OnDash += _dashController.OnDash;
            _changeWeaponInputProvider.OnChangeWeaponDown += _weaponChangeMechanic.OnChangeWeaponDown;
            _changeWeaponInputProvider.OnChangeWeaponUp += _weaponChangeMechanic.OnChangeWeaponUp;
        }

        private void Start()
        {
            //For test
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.Revolver, _weaponListParent.position, _weaponListParent));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.Shotgun, _weaponListParent.position, _weaponListParent));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.MachineGun, _weaponListParent.position, _weaponListParent));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.RocketLauncher, _weaponListParent.position, _weaponListParent));
        }

        private void OnEnable()
        {
            if (_changeWeaponInputProvider == null) return;
            if (_moveInputProvider == null) return;
            if (_dashInputProvider == null) return;
            if (_screenMouseMoveInputProvider == null) return;

            _moveInputProvider.OnMove += _moveController.OnMove;
            _dashInputProvider.OnDash += _dashController.OnDash;
            _changeWeaponInputProvider.OnChangeWeaponDown += _weaponChangeMechanic.OnChangeWeaponDown;
            _changeWeaponInputProvider.OnChangeWeaponUp += _weaponChangeMechanic.OnChangeWeaponUp;
        }

        private void OnDisable()
        {
            if (_changeWeaponInputProvider == null) return;
            if (_moveInputProvider == null) return;
            if (_dashInputProvider == null) return;
            if (_screenMouseMoveInputProvider == null) return;

            _moveInputProvider.OnMove -= _moveController.OnMove;
            _dashInputProvider.OnDash -= _dashController.OnDash;
            _changeWeaponInputProvider.OnChangeWeaponDown -= _weaponChangeMechanic.OnChangeWeaponDown;
            _changeWeaponInputProvider.OnChangeWeaponUp -= _weaponChangeMechanic.OnChangeWeaponUp;
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _moveComponent = GetComponent<Move2DComponent>();
            _moveController = GetComponent<UnitMoveMechanic>();
            _dashController = GetComponent<UnitDashMechanic>();
            _weaponChangeMechanic = GetComponent<WeaponChangeMechanic>();
            _weaponStorage = GetComponent<WeaponsStorage>();
            _healthComponent = GetComponent<HealthComponent>();
            _dieMechanic = GetComponent<UnitDieMechanic>();
        }
#endif
    }
}