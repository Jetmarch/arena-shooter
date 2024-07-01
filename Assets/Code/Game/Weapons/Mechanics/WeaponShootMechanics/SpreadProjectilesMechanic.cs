using UnityEngine;

namespace ArenaShooter.Weapons
{
    public class SpreadProjectilesMechanic : BaseWeaponShootMechanic
    {
        [SerializeField]
        private int _countOfProjectiles = 6;
        protected override void ShootMechanic()
        {
            var angleStep = 360 / _countOfProjectiles;
            for (int i = 0; i < _countOfProjectiles; i++)
            {
                var projectileRotation = Quaternion.Euler(0f, 0f, i * angleStep);
                _projectileFactory.CreateProjectile(_projectileType, transform.position, projectileRotation, _owner);
            }
        }
    }
}