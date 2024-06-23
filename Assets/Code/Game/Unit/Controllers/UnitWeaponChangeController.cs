using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using Zenject;

namespace ArenaShooter.Units.Player
{
    public class UnitWeaponChangeController : IInitializable, ILateDisposable
    {
        private WeaponChangeMechanic _weaponChangeMechanic;
        private IChangeWeaponInputProvider _changeWeaponInputProvider;

        public UnitWeaponChangeController(WeaponChangeMechanic weaponChangeMechanic, IChangeWeaponInputProvider changeWeaponInputProvider)
        {
            _weaponChangeMechanic = weaponChangeMechanic;
            _changeWeaponInputProvider = changeWeaponInputProvider;
        }

        public void Initialize()
        {
            _changeWeaponInputProvider.OnChangeWeaponUp += _weaponChangeMechanic.OnChangeWeaponUp;
            _changeWeaponInputProvider.OnChangeWeaponDown += _weaponChangeMechanic.OnChangeWeaponDown;
        }

        public void LateDispose()
        {
            _changeWeaponInputProvider.OnChangeWeaponUp -= _weaponChangeMechanic.OnChangeWeaponUp;
            _changeWeaponInputProvider.OnChangeWeaponDown -= _weaponChangeMechanic.OnChangeWeaponDown;
        }
    }
}