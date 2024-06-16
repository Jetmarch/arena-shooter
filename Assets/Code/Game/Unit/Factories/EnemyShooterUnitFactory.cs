using ArenaShooter.AI;
using ArenaShooter.Units.Enemies;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Factories
{
    public class EnemyShooterUnitFactory : BaseUnitFactory
    {

        private AIStateMachineFactory _aIStateMachineFactory;

        [Inject]
        private void Construct(AIStateMachineFactory aiStateMachineFactory)
        {
            _aIStateMachineFactory = aiStateMachineFactory;
        }

        public override GameObject CreateUnit(Vector3 position, Transform parent)
        {
            var enemy = Instantiate(_unitPrefab, position, _unitPrefab.transform.rotation, parent);
            var installer = enemy.GetComponent<EnemyShooterInstaller>();
            if(installer == null)
            {
                throw new Exception($"Enemy prefab {_unitPrefab.name} doesn't have EnemyShooterInstaller!");
            }

            installer.Construct(_aIStateMachineFactory);
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