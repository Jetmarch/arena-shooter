using ArenaShooter.Units.Player;
using ArenaShooter.Weapons;
using Zenject;

namespace ArenaShooter.Artefacts
{
    public class DropStunProjectileOnReloadController : IInitializable, ILateDisposable
    {
        private DropStunProjectileMechanic _mechanic;
        private WeaponsStorage _weaponStorage;

        public DropStunProjectileOnReloadController(DropStunProjectileMechanic mechanic, IPlayerProvider playerProvider)
        {
            _mechanic = mechanic;
            _weaponStorage = playerProvider.Player.WeaponsStorage;
        }

        public void Initialize()
        {
            foreach (var weapon in _weaponStorage.Weapons)
            {
                weapon.WeaponReloadMechanic.OnStartReload += _mechanic.DropStunProjectile;
            }
        }

        public void LateDispose()
        {
            foreach (var weapon in _weaponStorage.Weapons)
            {
                weapon.WeaponReloadMechanic.OnStartReload -= _mechanic.DropStunProjectile;
            }
        }
    }
}