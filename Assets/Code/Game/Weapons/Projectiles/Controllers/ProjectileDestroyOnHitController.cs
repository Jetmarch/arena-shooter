using ArenaShooter.Mechanics;
using Zenject;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileDestroyOnHitController : IInitializable, ILateDisposable
    {
        private ProjectileDestroyOnHitMechanic _destroyOnHitMechanic;
        private IDamageMechanic _damageMechanic;

        public ProjectileDestroyOnHitController(ProjectileDestroyOnHitMechanic destroyOnHitMechanic, IDamageMechanic damageMechanic)
        {
            _destroyOnHitMechanic = destroyOnHitMechanic;
            _damageMechanic = damageMechanic;
        }

        public void Initialize()
        {
            _damageMechanic.HitGameObject += _destroyOnHitMechanic.OnHit;
        }

        public void LateDispose()
        {
            _damageMechanic.HitGameObject -= _destroyOnHitMechanic.OnHit;
        }
    }
}