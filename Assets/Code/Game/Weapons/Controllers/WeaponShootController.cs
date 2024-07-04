using ArenaShooter.Inputs;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class WeaponShootController : IInitializable, ILateDisposable
    {
        private BaseWeaponShootMechanic _weaponShootMechanic;
        private IShootInputProvider _shootInputProvider;

        public WeaponShootController(BaseWeaponShootMechanic weaponShootMechanic, IShootInputProvider shootInputProvider)
        {
            _weaponShootMechanic = weaponShootMechanic;
            _shootInputProvider = shootInputProvider;
        }

        public void Initialize()
        {
            _shootInputProvider.OnShoot += _weaponShootMechanic.Shoot;
        }

        public void LateDispose()
        {
            _shootInputProvider.OnShoot -= _weaponShootMechanic.Shoot;
        }
    }
}