namespace ArenaShooter.Weapons
{
    public class ShootSingleProjectileMechanic : BaseWeaponShootMechanic
    {
        protected override void ShootMechanic()
        {
            _projectileFactory.CreateProjectile(_projectileType, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation, _owner);
        }
    }
}