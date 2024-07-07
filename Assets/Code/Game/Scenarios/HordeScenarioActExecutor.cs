using ArenaShooter.Components;
using ArenaShooter.Units;
using System;
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
        private bool _spawnInProcess;
        private int _diedUnits;

        public IReadOnlyCollection<GameObject> HordeUnits { get {  return _hordeUnits; } }
        public event Action HordeUnitDied;

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
                throw new Exception($"Type mismatch between ScenarioType and ScenarioActData type!");
            }

            _diedUnits = 0;
            _data = hordeData;
            OnScenarioActStart();
            StartCoroutine(SpawnHorde(_data));
        }

        public int GetCountOfUnitsInHorde()
        {
            if (_data == null) return 0;
            int countOfUnits = 0;

            foreach(var unitData in _data.EnemyData)
            {
                countOfUnits += unitData.CountOfEnemies;
            }

            return countOfUnits;
        }

        public int GetCountOfRemainingUnits()
        {
            return GetCountOfUnitsInHorde() - _diedUnits;
        }

        private IEnumerator SpawnHorde(HordeScenarioActData hordeData)
        {
            _spawnInProcess = true;
            foreach (var enemy in hordeData.EnemyData)
            {
                for (int i = 0; i < enemy.CountOfEnemies; i++)
                {
                    yield return new WaitForSeconds(_delayBetweenUnitSpawn);
                    var newUnit = _unitManager.CreateUnit(enemy.UnitType, _spawnPoints[_currentSpawnPoint].GetRandomPointInside(), null);
                    _hordeUnits.Add(newUnit);
                }
            }

            SetNextSpawnPoint();
            _spawnInProcess = false;
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
                _diedUnits++;
                _hordeUnits.Remove(obj);
                HordeUnitDied?.Invoke();
            }

            if (_hordeUnits.Count <= 0 && !_spawnInProcess)
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