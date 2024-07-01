using ArenaShooter.Weapons.Projectiles;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class SpreaderWeaponInstaller : MonoInstaller
    {
        private BaseWeaponShootMechanic _shootMechanic;

        public override void InstallBindings()
        {
            
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _shootMechanic = GetComponent<BaseWeaponShootMechanic>();
        }
#endif
    }
}