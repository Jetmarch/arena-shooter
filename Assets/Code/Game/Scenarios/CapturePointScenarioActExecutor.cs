using ArenaShooter.Components;
using ArenaShooter.Units;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Scenarios
{
    public class CapturePointScenarioActExecutor : BaseScenarioActExecutor
    {
        [SerializeField]
        private List<CapturePointComponent> _capturePoints;

        [SerializeField]
        private int _currentPoint = 0;

        [SerializeField]
        private int _maxUnits = 20;
        [SerializeField]
        private List<GameObject> _spawnedUnits;

        private UnitManager _unitManager;

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
        }

        private void OnPointCaptured()
        {
            CurrentPoint.PointCaptured -= OnPointCaptured;
            SetNextPoint();
            OnScenarioActFinish();

            Debug.Log("Point captured!");
        }

        private void SetNextPoint()
        {
            _currentPoint++;
            _currentPoint %= _capturePoints.Count;
        }
    }
}