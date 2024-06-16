using ArenaShooter.AI;
using ArenaShooter.Units.Enemies;
using ArenaShooter.Weapons.Projectiles;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Factories
{
    public class EnemyShooterUnitFactory : BaseUnitFactory
    {

        private AIStateMachineFactory _aIStateMachineFactory;
        private ProjectileFactory _projectileFactory;

        [Inject]
        private void Construct(AIStateMachineFactory aiStateMachineFactory, ProjectileFactory projectileFactory)
        {
            _aIStateMachineFactory = aiStateMachineFactory;
            _projectileFactory = projectileFactory;
        }

        public override GameObject CreateUnit(Vector3 position, Transform parent)
        {
            var enemy = Instantiate(_unitPrefab, position, _unitPrefab.transform.rotation, parent);
            var installer = enemy.GetComponent<EnemyShooterInstaller>();
            if(installer == null)
            {
                throw new Exception($"Enemy prefab {_unitPrefab.name} doesn't have EnemyShooterInstaller!");
            }

            installer.Construct(_aIStateMachineFactory, _projectileFactory);
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