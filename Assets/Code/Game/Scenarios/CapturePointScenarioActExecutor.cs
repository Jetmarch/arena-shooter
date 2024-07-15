using ArenaShooter.Components;
using ArenaShooter.Units;
using ArenaShooter.Units.Factories;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Scenarios
{
    public class CapturePointScenarioActExecutor : BaseScenarioActExecutor, IGamePauseListener
    {
        [SerializeField]
        private List<CapturePointComponent> _capturePoints;

        [SerializeField]
        private List<SpawnPointComponent> _spawnPoints;

        [SerializeField]
        private List<UnitType> _unitsToSpawn;

        [SerializeField]
        private int _currentCapturePoint = 0;

        [SerializeField]
        private int _currentSpawnPoint = 0;

        [SerializeField]
        private int _maxUnits = 20;
        [SerializeField]
        private float _timeBetweenSpawnUnits = 0.5f;
        [SerializeField]
        private List<GameObject> _spawnedUnits;
        private Coroutine _spawnEnemiesCoroutine;

        private UnitManager _unitManager;

        private bool _isPaused;

        public CapturePointComponent CurrentCapturePoint { get { return _capturePoints[_currentCapturePoint]; } }
        public SpawnPointComponent CurrentSpawnPoint { get { return _spawnPoints[_currentSpawnPoint]; } }


        [Inject]
        private void Construct(UnitManager unitManager)
        {
            _scenarioType = ScenarioType.CapturePoint;
            _unitManager = unitManager;
            _spawnedUnits = new List<GameObject>();
        }

        public override void Execute(BaseScenarioActData data)
        {
            var pointData = data as CapturePointScenarioActData;
            if (pointData == null)
            {
                throw new Exception("Type mismatch between ScenarioType and BaseScenarioActData type!");
            }

            OnScenarioActStart();
            CurrentCapturePoint.ActivatePoint();
            CurrentCapturePoint.PointCaptured += OnPointCaptured;
            _spawnEnemiesCoroutine = StartCoroutine(SpawnEnemies());
            _unitManager.UnitDie += OnSpawnedEnemyDie;
        }

        private void OnSpawnedEnemyDie(GameObject obj)
        {
            if (_spawnedUnits.Contains(obj))
            {
                _spawnedUnits.Remove(obj);
            }
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                if (_spawnedUnits.Count < _maxUnits)
                {
                    //Just for test
                    var newEnemy = _unitManager.CreateUnit(GetRandomUnitType(), CurrentSpawnPoint.GetRandomPointInside(), null);
                    _spawnedUnits.Add(newEnemy);
                    SetNextSpawnPoint();
                }
                yield return new WaitForSeconds(_timeBetweenSpawnUnits);
                yield return new WaitUntil(() => _isPaused == false);
            }
        }

        private void OnPointCaptured()
        {
            CurrentCapturePoint.PointCaptured -= OnPointCaptured;
            _unitManager.UnitDie -= OnSpawnedEnemyDie;
            SetNextCapturePoint();
            OnScenarioActFinish();
            StopCoroutine(_spawnEnemiesCoroutine);
            KillAllSpawnedEnemies();
            Debug.Log("Point captured!");
        }

        private void KillAllSpawnedEnemies()
        {
            for (int i = 0; i < _spawnedUnits.Count; i++)
            {
                var health = _spawnedUnits[i].GetComponent<HealthComponent>();
                if (health == null)
                {
                    Debug.LogWarning($"Unit {_spawnedUnits[i].name} don't have HealthComponent!");
                    continue;
                }

                health.SetCurrentHealth(health.MinHealth);
            }

            _spawnedUnits.Clear();
        }

        private void SetNextCapturePoint()
        {
            _currentCapturePoint++;
            _currentCapturePoint %= _capturePoints.Count;
        }

        private void SetNextSpawnPoint()
        {
            _currentSpawnPoint++;
            _currentSpawnPoint %= _spawnPoints.Count;
        }

        private UnitType GetRandomUnitType()
        {
            return _unitsToSpawn[UnityEngine.Random.Range(0, _unitsToSpawn.Count)];
        }

        public void OnPauseGame()
        {
            _isPaused = true;
        }

        public void OnResumeGame()
        {
            _isPaused = false;
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }
    }
}