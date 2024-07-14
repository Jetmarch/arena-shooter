using ArenaShooter.Projectiles;
using ArenaShooter.Units.Player;

namespace ArenaShooter.Artefacts
{
    public class DropStunProjectileMechanic
    {
        private ProjectileFactory _projectileFactory;
        private PlayerFacade _playerFacade;

        public DropStunProjectileMechanic(ProjectileFactory projectileFactory, IPlayerProvider playerProvider)
        {
            _projectileFactory = projectileFactory;
            _playerFacade = playerProvider.Player;
        }

        public void DropStunProjectile()
        {
            _projectileFactory.CreateProjectile(ProjectileType.StunProjectile, _playerFacade.CurrentWeapon.Position, _playerFacade.CurrentWeapon.Rotation, _playerFacade.gameObject);
        }
    }
}