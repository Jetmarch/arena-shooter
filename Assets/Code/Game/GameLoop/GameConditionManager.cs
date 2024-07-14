using ArenaShooter.Scenarios;
using ArenaShooter.Units.Player;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter
{
    public class GameConditionManager : IInitializable, ILateDisposable
    {
        private ArenaScenarioExecutor _scenarioExecutor;
        private IPlayerProvider _playerProvider;
        private GameCondition _gameCondition;

        public Action OnVictory;
        public Action OnDefeat;
        public GameCondition GameCondition { get { return _gameCondition; } }

        public GameConditionManager(ArenaScenarioExecutor scenarioExecutor, IPlayerProvider playerProvider)
        {
            _scenarioExecutor = scenarioExecutor;
            _playerProvider = playerProvider;
            _gameCondition = GameCondition.Battle;
        }

        public void Initialize()
        {
            _scenarioExecutor.OnScenarioFinish += Victory;
            _playerProvider.OnPlayerDied += Defeat;
        }

        public void LateDispose()
        {
            _scenarioExecutor.OnScenarioFinish -= Victory;
            _playerProvider.OnPlayerDied -= Defeat;
        }

        private void Victory()
        {
            if (_gameCondition != GameCondition.Battle) return;
            OnVictory?.Invoke();
            _gameCondition = GameCondition.Victory;
        }

        private void Defeat(PlayerFacade _)
        {
            if (_gameCondition != GameCondition.Battle) return;
            OnDefeat?.Invoke();
            _gameCondition = GameCondition.Battle;
        }
    }
}