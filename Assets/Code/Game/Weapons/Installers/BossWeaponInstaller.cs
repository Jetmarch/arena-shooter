using ArenaShooter.Weapons.Projectiles;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    public class BossWeaponInstaller : MonoBehaviour
    {
        private BaseWeaponShootMechanic _shootMechanic;

        public void Construct(ProjectileFactory _projectileFactory)
        {
            _shootMechanic.Construct(_projectileFactory);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _shootMechanic = GetComponent<BaseWeaponShootMechanic>();
        }
#endif
    }
}