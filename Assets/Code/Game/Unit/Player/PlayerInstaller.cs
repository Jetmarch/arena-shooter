using ArenaShooter.Components;
using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using ArenaShooter.Weapons.Projectiles;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Player
{
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
        private Transform _weaponListParent;

        private WeaponFactory _weaponFactory;

        //TODO: Возможно конструировать игрока из фабрики
        [Inject]
        private void Construct(IMoveInputProvider moveInputProvider, IDashInputProvider dashInputProvider, IShootInputProvider shootInputProvider, IChangeWeaponInputProvider changeWeaponInputProvider, WeaponFactory weaponFactory)
        {
            _moveComponent.Construct(_rigidbody);
            _moveController.Constuct(moveInputProvider, _moveComponent);
            
            _weaponChangeMechanic.Construct(changeWeaponInputProvider, _weaponStorage);
            _dashController.Construct(dashInputProvider, _moveComponent);

            _moveController.Condition.Append(_dashController.IsNotDashing);

            _weaponFactory = weaponFactory;
        }

        private void Start()
        {
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.Revolver, _weaponListParent.position, _weaponListParent));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(WeaponType.Shotgun, _weaponListParent.position, _weaponListParent));
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
        }
#endif
    }
}