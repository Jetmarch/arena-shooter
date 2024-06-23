using ArenaShooter.Units.Enemies;
using ArenaShooter.Weapons.Projectiles;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Factories
{
    public class EnemyShooterUnitFactory : BaseUnitFactory
    {
        private ProjectileFactory _projectileFactory;
        private DiContainer _container;

        [Inject]
        private void Construct(ProjectileFactory projectileFactory, DiContainer container)
        {
            _projectileFactory = projectileFactory;
            _container = container;
        }

        public override GameObject CreateUnit(Vector3 position, Transform parent)
        {
            //var enemy = Instantiate(_unitPrefab, position, _unitPrefab.transform.rotation, parent);
            //var installer = enemy.GetComponent<EnemyShooterInstaller>();
            //if (installer == null)
            //{
            //    throw new Exception($"Enemy prefab {_unitPrefab.name} doesn't have EnemyShooterInstaller!");
            //}

            //installer.Construct(_projectileFactory);

            var enemy = _container.InstantiatePrefab(_unitPrefab, position, _unitPrefab.transform.rotation, parent);
            return enemy;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _unitType = UnitType.EnemyShooter;
        }
#endif
    }
}