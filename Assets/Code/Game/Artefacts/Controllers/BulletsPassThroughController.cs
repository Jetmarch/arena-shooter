using ArenaShooter.Projectiles;
using Zenject;

namespace ArenaShooter.Artefacts
{
    public class BulletsPassThroughController : IInitializable, ILateDisposable
    {
        private BulletsPassThroughMechanic _mechanic;
        private ProjectileFactory _projectileFactory;

        public BulletsPassThroughController(BulletsPassThroughMechanic mechanic, ProjectileFactory projectileFactory)
        {
            _mechanic = mechanic;
            _projectileFactory = projectileFactory;
        }

        public void Initialize()
        {
            _projectileFactory.OnProjectileCreated += _mechanic.SetProjectileHitCount;
        }

        public void LateDispose()
        {
            _projectileFactory.OnProjectileCreated -= _mechanic.SetProjectileHitCount;
        }
    }
}