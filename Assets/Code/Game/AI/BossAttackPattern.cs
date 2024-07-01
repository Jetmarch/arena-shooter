using ArenaShooter.Weapons;
using UnityEngine;

namespace ArenaShooter.AI
{
    public class BossAttackPattern
    {
        private WeaponChangeMechanic _weaponChangeMechanic;

        public BossAttackPattern(WeaponChangeMechanic weaponChangeMechanic)
        {
            _weaponChangeMechanic = weaponChangeMechanic;
        }

        public void OnAttack()
        {
            var weapon = _weaponChangeMechanic.CurrentWeapon;
            weapon.WeaponShootMechanic.OnShoot();
            _weaponChangeMechanic.OnChangeWeaponDown();
        }
    }
}