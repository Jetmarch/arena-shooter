using ArenaShooter.Units.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter
{
    public class PauseGameOnDefeatController : IInitializable, ILateDisposable
    {
        private GameLoopManager _gameLoopManager;
        private IPlayerProvider _playerProvider;

        public PauseGameOnDefeatController(GameLoopManager gameLoopManager, IPlayerProvider playerProvider)
        {
            _gameLoopManager = gameLoopManager;
            _playerProvider = playerProvider;
        }

        public void Initialize()
        {
            _playerProvider.OnPlayerDied += OnPlayerDied;
        }

        public void LateDispose()
        {
            _playerProvider.OnPlayerDied -= OnPlayerDied;
        }

        private void OnPlayerDied(PlayerFacade _)
        {
            _gameLoopManager.PauseGame();
        }
    }
}