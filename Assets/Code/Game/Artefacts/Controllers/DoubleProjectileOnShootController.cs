using ArenaShooter.Units.Player;
using ArenaShooter.Weapons;
using Zenject;

namespace ArenaShooter.Artefacts
{
    public class DoubleProjectileOnShootController : IInitializable, ILateDisposable
    {
        private DoubleProjectileMechanic _mechanic;
        private WeaponsStorage _weaponsStorage;

        public DoubleProjectileOnShootController(DoubleProjectileMechanic mechanic, IPlayerProvider playerProvider)
        {
            _mechanic = mechanic;
            _weaponsStorage = playerProvider.Player.WeaponsStorage;
        }

        public void Initialize()
        {
            foreach (var weapon in _weaponsStorage.Weapons)
            {
                weapon.WeaponShootMechanic.ShootComplete += _mechanic.DoubleShot;
            }
        }

        public void LateDispose()
        {
            foreach (var weapon in _weaponsStorage.Weapons)
            {
                weapon.WeaponShootMechanic.ShootComplete -= _mechanic.DoubleShot;
            }
        }
    }
}