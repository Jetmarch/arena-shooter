using ArenaShooter.Mechanics;
using Zenject;

namespace ArenaShooter.Projectiles
{
    public class ProjectileImpactAfterDelayController : IInitializable, ILateDisposable
    {
        private ProjectileImpactAfterDelayMechanic _impactMechanic;
        private IDamageMechanic _damageMechanic;

        public ProjectileImpactAfterDelayController(ProjectileImpactAfterDelayMechanic impactMechanic, IDamageMechanic damageMechanic)
        {
            _impactMechanic = impactMechanic;
            _damageMechanic = damageMechanic;
        }

        public void Initialize()
        {
            _impactMechanic.OnImpact += OnImpact;
        }

        public void LateDispose()
        {
            _impactMechanic.OnImpact -= OnImpact;
        }

        private void OnImpact()
        {
            _damageMechanic.OnHit(null);
        }
    }
}