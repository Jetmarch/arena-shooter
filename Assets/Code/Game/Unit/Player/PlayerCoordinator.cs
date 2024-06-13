using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using ArenaShooter.Weapons.Projectiles;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Player
{
    public class PlayerCoordinator : MonoBehaviour
    {
        [SerializeField]
        private UnitConditionContainer _conditionContainer;
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
        private void Construct(IMoveInputProvider moveInputProvider, IShootInputProvider shootInputProvider, IChangeWeaponInputProvider changeWeaponInputProvider, WeaponFactory weaponFactory)
        {
            _moveController.Constuct(moveInputProvider);
            _weaponChangeMechanic.Construct(changeWeaponInputProvider, _weaponStorage);

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
            _conditionContainer = GetComponent<UnitConditionContainer>();
            _moveController = GetComponent<UnitMoveMechanic>();
            _dashController = GetComponent<UnitDashMechanic>();
            _weaponChangeMechanic = GetComponent<WeaponChangeMechanic>();
            _weaponStorage = GetComponent<WeaponsStorage>();
        }
#endif
    }
}