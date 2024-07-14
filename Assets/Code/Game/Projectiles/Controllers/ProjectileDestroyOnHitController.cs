using ArenaShooter.Mechanics;
using Zenject;

namespace ArenaShooter.Projectiles
{
    public class ProjectileDestroyOnHitController : IInitializable, ILateDisposable
    {
        private ProjectileDestroyMechanic _destroyOnHitMechanic;
        private IImpactMechanic _damageMechanic;

        public ProjectileDestroyOnHitController(ProjectileDestroyMechanic destroyOnHitMechanic, IImpactMechanic damageMechanic)
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