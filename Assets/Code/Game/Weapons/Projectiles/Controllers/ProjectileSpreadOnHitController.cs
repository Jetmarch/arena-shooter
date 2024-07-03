using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileSpreadOnHitController : IInitializable
    {
        private IProjectileDamageMechanic _damageMechanic;
        private SpreadProjectilesMechanic _spreadProjectileMechanic;

        public ProjectileSpreadOnHitController(IProjectileDamageMechanic damageMechanic, SpreadProjectilesMechanic spreadProjectileMechanic)
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