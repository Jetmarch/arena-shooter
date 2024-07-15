using ArenaShooter.Mechanics;
using ArenaShooter.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Projectiles
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

        public Action<ProjectileFacade, GameObject> OnProjectileCreated;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        public ProjectileFacade CreateProjectile(ProjectileType type, Vector3 position, Quaternion rotation, GameObject owner)
        {
            var projectile = _projectiles.Find(x => x.Type == type).ProjectilePrefab;
            if (projectile == null)
            {
                throw new Exception($"ProjectileFactory: Type {type} does not contain prefab object!");
            }
            //TODO: Перенести в пул
            var createdProjectile = _container.InstantiatePrefab(projectile, position, rotation, _projectilePool);

            var damageMechanic = createdProjectile.GetComponent<IImpactMechanic>();
            if (damageMechanic == null)
            {
                throw new Exception($"ProjectileFactory: Projectile with {type} does not contain IProjectileDamageMechanic!");
            }

            damageMechanic.Owner = owner;

            var spreadProjectiles = createdProjectile.GetComponent<SpreadProjectilesMechanic>();
            if (spreadProjectiles != null)
            {
                spreadProjectiles.SetOwner(owner);
            }
            var projectileFacade = createdProjectile.GetComponent<ProjectileFacade>();
            if (projectileFacade == null)
            {
                throw new Exception($"ProjectileFactory: Projectile with {type} does not contain ProjectileFacade!");
            }

            OnProjectileCreated?.Invoke(projectileFacade, owner);

            return projectileFacade;
        }
    }
}