using ArenaShooter.Mechanics;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileSpreadOnHitController : IInitializable
    {
        private IDamageMechanic _damageMechanic;
        private SpreadProjectilesMechanic _spreadProjectileMechanic;

        public ProjectileSpreadOnHitController(IDamageMechanic damageMechanic, SpreadProjectilesMechanic spreadProjectileMechanic)
        {
            _damageMechanic = damageMechanic;
            _spreadProjectileMechanic = spreadProjectileMechanic;
        }

        public void Initialize()
        {
            _damageMechanic.HitGameObject += OnShoot;
        }

        private void OnShoot(GameObject obj)
        {
            _spreadProjectileMechanic.Shoot();
        }
    }
}