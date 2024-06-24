using UnityEngine;

namespace ArenaShooter.Weapons
{
    public class ShootProjectileBurstMechanic : BaseWeaponShootMechanic
    {
        [SerializeField]
        private int _countOfProjectilesInShot = 4;

        [SerializeField]
        private float _minSpreadAngle = -5f;
        [SerializeField]
        private float _maxSpreadAngle = 5f;

        protected override void ShootMechanic()
        {
            for (int i = 0; i < _countOfProjectilesInShot; i++)
            {
                var spreadRotation = Quaternion.Euler(_projectileSpawnPoint.eulerAngles.x, _projectileSpawnPoint.eulerAngles.y, _projectileSpawnPoint.eulerAngles.z + Random.Range(_minSpreadAngle, _maxSpreadAngle));

                //TODO: Передавать пул снарядов в качестве родительского объекта
                _projectileFactory.CreateProjectile(_projectileType, _projectileSpawnPoint.position, spreadRotation, gameObject);
            }
        }
    }
}