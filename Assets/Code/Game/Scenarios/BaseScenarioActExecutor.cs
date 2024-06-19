using System;
using UnityEngine;

namespace ArenaShooter.Scenarios
{
    public abstract class BaseScenarioActExecutor : MonoBehaviour
    {
        protected ScenarioType _scenarioType;
        protected bool _isActive;
        public ScenarioType ScenarioType { get { return _scenarioType; } }
        public event Action ScenarioActStart;
        public event Action ScenarioActFinish;

        public abstract void Execute(BaseScenarioActData data);

        protected virtual void OnScenarioActStart()
        {
            _isActive = true;
            ScenarioActStart?.Invoke();
        }

        protected virtual void OnScenarioActFinish()
        {
            _isActive = false;
            ScenarioActFinish?.Invoke();
        }
    }
}