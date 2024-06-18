using System;
using UnityEngine;

namespace ArenaShooter.Scenarios
{
    public abstract class BaseScenarioActExecutor : MonoBehaviour
    {
        protected ScenarioType _scenarioType;

        public ScenarioType ScenarioType { get { return _scenarioType; } }
        public event Action ScenarioActStart;
        public event Action ScenarioActFinish;

        public abstract void Execute(BaseScenarioActData data);

        protected void OnScenarioActStart()
        {
            ScenarioActStart?.Invoke();
        }

        protected void OnScenarioActFinish()
        {
            ScenarioActFinish?.Invoke();
        }
    }
}