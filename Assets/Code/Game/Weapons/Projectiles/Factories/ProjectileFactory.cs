using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileFactory : MonoBehaviour
    {

        [Serializable]
        public struct ProjectileFactoryData
        {
            public ProjectileType Type;
            public GameObject ProjectilePrefab;
        }

        [SerializeField]
        private List<ProjectileFactoryData> _projectiles;

        [SerializeField]
        private Transform _projectilePool;

        public GameObject CreateProjectile(ProjectileType type, Vector3 position, Quaternion rotation)
        {
            var projectile = _projectiles.Find(x => x.Type == type).ProjectilePrefab;
            if (projectile == null)
            {
                throw new Exception($"ProjectileFactory: Type {type} does not contain prefab object!");
            }

            var createdProjectile = Instantiate(projectile, position, rotation, _projectilePool);
            createdProjectile.GetComponent<ProjectileInstaller>().Construct();
            return createdProjectile;
        }
    }
}