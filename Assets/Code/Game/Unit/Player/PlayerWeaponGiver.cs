using ArenaShooter.Weapons;
using Zenject;

namespace ArenaShooter.Units.Player
{
    public class PlayerWeaponGiver
    {
        private WeaponsStorage _weaponStorage;
        private PlayerWeaponFactory _weaponFactory;
        private WeaponSet _weaponSet;

        public PlayerWeaponGiver(WeaponsStorage weaponStorage, PlayerWeaponFactory weaponFactory, WeaponSet weaponSet)
        {
            _weaponStorage = weaponStorage;
            _weaponFactory = weaponFactory;
            _weaponSet = weaponSet;
        }

        public void GiveWeapon()
        {
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(_weaponSet.PrimaryWeapon));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(_weaponSet.SecondaryWeapon));
        }
    }
}