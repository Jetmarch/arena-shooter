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
            _canInteract = true;
        }

        public void Activate(GameObject _)
        {
            if (!CanInteract()) return;
            _inputProvider.OnInteract += Interact;
            OnActivate?.Invoke();
        }

        public void Deactivate(GameObject _)
        {
            _inputProvider.OnInteract -= Interact;
            OnDeactivate?.Invoke();
        }

        public bool CanInteract()
        {
            return _canInteract;
        }

        public void Interact()
        {
            if (!CanInteract()) return;

            _gameLoopManager.StartGame();
            _canInteract = false;
        }
    }
}