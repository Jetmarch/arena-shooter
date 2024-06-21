using System;

namespace ArenaShooter.Weapons
{
    [Obsolete]
    public class ShootAutoFireMechanic : BaseWeaponShootMechanic
    {
        public override void ShootMechanic()
        {
            _projectileFactory.CreateProjectile(_projectileType, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
        }
    }
}