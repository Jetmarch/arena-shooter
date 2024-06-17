namespace ArenaShooter.Weapons
{
    public class ShootSingleProjectileObserver : BaseWeaponShootMechanic
    {
        public override void OnShoot()
        {
            if (!CanShoot()) return;

            _projectileFactory.CreateProjectile(_projectileType, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
        }
    }
}