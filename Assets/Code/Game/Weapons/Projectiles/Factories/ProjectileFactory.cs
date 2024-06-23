using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

        private DiContainer _container;

        [SerializeField]
        private List<ProjectileFactoryData> _projectiles;

        [SerializeField]
        private Transform _projectilePool;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        public GameObject CreateProjectile(ProjectileType type, Vector3 position, Quaternion rotation)
        {
            var projectile = _projectiles.Find(x => x.Type == type).ProjectilePrefab;
            if (projectile == null)
            {
                throw new Exception($"ProjectileFactory: Type {type} does not contain prefab object!");
            }
            //TODO: Перенести в пул
            var createdProjectile = _container.InstantiatePrefab(projectile, position, rotation, _projectilePool);

            return createdProjectile;
        }
    }
}