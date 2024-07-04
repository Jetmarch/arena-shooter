using Zenject;

namespace ArenaShooter.Weapons
{
    public class AmmoInClipDecreaseController : IInitializable, ILateDisposable
    {
        private AmmoInClipDecreaseMechanic _ammoDecreaseMechanic;
        private BaseWeaponShootMechanic _weaponShootMechanic;

        public AmmoInClipDecreaseController(AmmoInClipDecreaseMechanic ammoDecreaseMechanic, BaseWeaponShootMechanic weaponShootMechanic)
        {
            _ammoDecreaseMechanic = ammoDecreaseMechanic;
            _weaponShootMechanic = weaponShootMechanic;
        }

        public void Initialize()
        {
            _weaponShootMechanic.ShootComplete += _ammoDecreaseMechanic.DecreaseAmmo;
        }

        public void LateDispose()
        {
            _weaponShootMechanic.ShootComplete -= _ammoDecreaseMechanic.DecreaseAmmo;
        }
    }
}