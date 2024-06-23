using ArenaShooter.Weapons;
using UnityEngine;

namespace ArenaShooter.AI
{
    public class BossAttackPattern : MonoBehaviour
    {
        private WeaponChangeMechanic _weaponChangeMechanic;

        public void Construct(WeaponChangeMechanic weaponChangeMechanic)
        {
            _weaponChangeMechanic = weaponChangeMechanic;
        }

        public void OnAttack()
        {
            var weapon = _weaponChangeMechanic.CurrentWeapon;
            var shootMechanic = weapon.GetComponent<BaseWeaponShootMechanic>();
            if(shootMechanic == null)
            {
                Debug.LogError($"Weapon {weapon.name} has no BaseWeaponShootMechanic!");
            }

            shootMechanic.OnShoot();
            _weaponChangeMechanic.OnChangeWeaponUp();
        }
    }
}