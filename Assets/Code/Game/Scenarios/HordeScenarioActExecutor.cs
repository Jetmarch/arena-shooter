using ArenaShooter.Components;
using ArenaShooter.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Scenarios
{
    public class HordeScenarioActExecutor : BaseScenarioActExecutor
    {
        [SerializeField]
        private List<GameObject> _hordeUnits = new();

        [SerializeField]
        private SpawnPointComponent[] _spawnPoints;

        [SerializeField]
        private float _delayBetweenUnitSpawn = 1f;

        [SerializeField]
        private int _currentSpawnPoint;

        private UnitManager _unitManager;
        private HordeScenarioActData _data;

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
            if (hordeData == null)
            {
                throw new System.Exception($"Type mismatch between ScenarioType and ScenarioActData type!");
            }

            OnScenarioActStart();
            _data = hordeData;
            StartCoroutine(SpawnHorde(_data));
        }

        private IEnumerator SpawnHorde(HordeScenarioActData hordeData)
        {
            foreach (var enemy in hordeData.EnemyData)
            {
                for (int i = 0; i < enemy.CountOfEnemies; i++)
                {
                    var newUnit = _unitManager.CreateUnit(enemy.UnitType, _spawnPoints[_currentSpawnPoint].GetRandomPointInside(), null);
                    _hordeUnits.Add(newUnit);
                    yield return new WaitForSeconds(_delayBetweenUnitSpawn);
                }
            }

            SetNextSpawnPoint();
        }

        private void SetNextSpawnPoint()
        {
            _currentSpawnPoint++;
            if (_currentSpawnPoint >= _spawnPoints.Length)
            {
                _currentSpawnPoint = 0;
            }
        }

        protected override void OnScenarioActStart()
        {
            _unitManager.UnitDie += OnHordeUnitDie;
            base.OnScenarioActStart();
        }

        private void OnHordeUnitDie(GameObject obj)
        {
            if (_hordeUnits.Contains(obj))
            {
                _hordeUnits.Remove(obj);
            }

            if (_hordeUnits.Count <= 0)
            {
                OnScenarioActFinish();
            }
        }

        protected override void OnScenarioActFinish()
        {
            _unitManager.UnitDie -= OnHordeUnitDie;
            base.OnScenarioActFinish();
        }
    }
}