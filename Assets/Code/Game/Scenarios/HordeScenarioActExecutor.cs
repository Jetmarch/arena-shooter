using ArenaShooter.Units;
using System.Collections;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Scenarios
{
    public class HordeScenarioActExecutor : BaseScenarioActExecutor
    {
        [SerializeField]
        private Transform[] _spawnPoints;

        [SerializeField]
        private float _delayBetweenSpawns = 1f;

        [SerializeField]
        private int _currentSpawnPoint;

        private UnitManager _unitManager;

        [Inject]
        private void Construct(UnitManager unitManager)
        {
            _unitManager = unitManager;
            _scenarioType = ScenarioType.Horde;
            _currentSpawnPoint = 0;
        }

        public override void Execute(BaseScenarioActData data)
        {
            var hordeData = data as HordeScenarioActData;
            if(hordeData == null)
            {
                throw new System.Exception($"Mismatch ScenarioType and ScenarioActData type!");
            }

            StartCoroutine(SpawnHorde(hordeData));

            OnScenarioActStart();
        }

        private IEnumerator SpawnHorde(HordeScenarioActData hordeData)
        {
            foreach (var enemy in hordeData.EnemyData)
            {
                for (int i = 0; i < enemy.CountOfEnemies; i++)
                {
                    _unitManager.CreateUnit(enemy.UnitType, _spawnPoints[_currentSpawnPoint].position, null);
                    yield return new WaitForSeconds(_delayBetweenSpawns);
                }
            }
        }

        private void GetNextSpawnPoint()
        {
            //
        }
    }
}