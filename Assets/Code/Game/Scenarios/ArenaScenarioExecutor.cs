using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Scenarios
{
    public class ArenaScenarioExecutor : MonoBehaviour, IGameStartListener
    {
        [SerializeField]
        private int _currentScenarioAct;

        private List<BaseScenarioActData> _scenarioActData;
        private List<BaseScenarioActExecutor> _scenarioActExecutors;

        public event Action OnScenarioStart;
        public event Action OnScenarioFinish;

        private void Awake()
        {
            _currentScenarioAct = 0;
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }

        [Inject]
        private void Construct(List<BaseScenarioActData> scenarioActs, List<BaseScenarioActExecutor> scenarioActExecutors)
        {
            _scenarioActData = scenarioActs;
            _scenarioActExecutors = scenarioActExecutors;
        }

        public void OnStartGame()
        {
            _scenarioActExecutors[_currentScenarioAct].Execute(_scenarioActData[_currentScenarioAct]);
            OnScenarioStart?.Invoke();
        }
    }
}