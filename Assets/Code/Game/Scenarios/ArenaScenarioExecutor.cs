using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Scenarios
{
    public class ArenaScenarioExecutor : MonoBehaviour, IGameStartListener
    {
        [SerializeField]
        private int _currentScenarioAct;

        [SerializeField]
        private float _delayBetweenActs = 3f;

        private List<BaseScenarioActData> _scenarioActData;
        private List<BaseScenarioActExecutor> _scenarioActExecutors;

        public event Action OnScenarioStart;
        public event Action OnScenarioFinish;

        public BaseScenarioActData CurrentScenarioActData => _scenarioActData[_currentScenarioAct];

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

            foreach (var actExecutor in scenarioActExecutors)
            {
                actExecutor.ScenarioActStart += OnScenarioActStart;
                actExecutor.ScenarioActFinish += OnScenarioActFinish;
            }
        }

        private void OnScenarioActFinish()
        {
            Debug.Log("Scenario act finish");
            _currentScenarioAct++;
            StartCoroutine(ExecuteCurrentAct());
        }

        private void OnScenarioActStart()
        {
            Debug.Log("Scenario act start");
        }

        public void OnStartGame()
        {
            OnScenarioStart?.Invoke();
            StartCoroutine(ExecuteCurrentAct());
        }

        private IEnumerator ExecuteCurrentAct()
        {
            if (CanExecute())
            {
                yield return new WaitForSeconds(_delayBetweenActs);
                var actData = _scenarioActData[_currentScenarioAct];
                foreach (var actExecutor in _scenarioActExecutors)
                {
                    if (actExecutor.ScenarioType == actData.Type)
                    {
                        actExecutor.Execute(actData);
                        yield break;
                    }
                }
            }
            else
            {
                Debug.Log("Scenario end!");
                OnScenarioFinish?.Invoke();
                yield break;
            }
        }

        private bool CanExecute()
        {
            return _currentScenarioAct < _scenarioActData.Count;
        }
    }
}