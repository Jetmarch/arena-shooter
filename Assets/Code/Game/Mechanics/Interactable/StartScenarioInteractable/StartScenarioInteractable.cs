using ArenaShooter.Inputs;
using System;
using UnityEngine;

namespace ArenaShooter.Mechanics
{
    public class StartScenarioInteractable : IInteractable
    {
        private GameLoopManager _gameLoopManager;

        private IInteractInputProvider _inputProvider;

        private bool _canInteract;

        public event Action OnActivate;
        public event Action OnDeactivate;

        public StartScenarioInteractable(GameLoopManager gameLoopManager, IInteractInputProvider inputProvider)
        {
            _gameLoopManager = gameLoopManager;
            _inputProvider = inputProvider;
            _canInteract = false;
        }

        public void Activate(GameObject _)
        {
            if (_gameLoopManager.State != GameState.None) return;

            _inputProvider.OnInteract += Interact;
            _canInteract = true;
            OnActivate?.Invoke();
        }

        public void Deactivate(GameObject _)
        {
            _inputProvider.OnInteract -= Interact;
            _canInteract = false;
            OnDeactivate?.Invoke();
        }

        public bool CanInteract()
        {
            return _canInteract;
        }

        public void Interact()
        {
            if (!CanInteract()) return;
            if (_gameLoopManager.State != GameState.None) return;

            _gameLoopManager.StartGame();
        }
    }
}