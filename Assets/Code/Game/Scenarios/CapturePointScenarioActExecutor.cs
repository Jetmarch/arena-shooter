using ArenaShooter.Components;
using ArenaShooter.Units;
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
        private int _currentPoint = 0;

        [SerializeField]
        private int _maxUnits = 20;
        [SerializeField]
        private float _timeBetweenSpawnUnits = 0.5f;
        [SerializeField]
        private List<GameObject> _spawnedUnits;
        private Coroutine _spawnEnemiesCoroutine;

        private UnitManager _unitManager;

        private bool _isPaused;

        public CapturePointComponent CurrentPoint { get { return _capturePoints[_currentPoint]; } }


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
            CurrentPoint.ActivatePoint();
            CurrentPoint.PointCaptured += OnPointCaptured;
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
                    var newEnemy = _unitManager.CreateUnit(Units.Factories.UnitType.EnemyShooter, _spawnPoints[0].GetRandomPointInside(), null);
                    _spawnedUnits.Add(newEnemy);
                }
                yield return new WaitForSeconds(_timeBetweenSpawnUnits);
                yield return new WaitUntil(() => _isPaused == false);
            }
        }

        private void OnPointCaptured()
        {
            CurrentPoint.PointCaptured -= OnPointCaptured;
            _unitManager.UnitDie -= OnSpawnedEnemyDie;
            SetNextPoint();
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

        private void SetNextPoint()
        {
            _currentPoint++;
            _currentPoint %= _capturePoints.Count;
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