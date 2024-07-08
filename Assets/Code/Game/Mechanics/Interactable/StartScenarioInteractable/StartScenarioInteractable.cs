using ArenaShooter.Inputs;
using UnityEngine;

namespace ArenaShooter.Mechanics
{
    public class StartScenarioInteractable : IInteractable
    {
        private GameLoopManager _gameLoopManager;

        private IInteractInputProvider _inputProvider;

        private bool _canInteract;

        public StartScenarioInteractable(GameLoopManager gameLoopManager, IInteractInputProvider inputProvider)
        {
            _gameLoopManager = gameLoopManager;
            _inputProvider = inputProvider;
            _canInteract = false;
        }

        public void Activate(GameObject _)
        {
            _inputProvider.OnInteract += Interact;
            _canInteract = true;
        }

        public void Deactivate(GameObject _)
        {
            _inputProvider.OnInteract -= Interact;
            _canInteract = false;
        }

        public bool CanInteract()
        {
            return _canInteract;
        }

        public void Interact()
        {
            if (!CanInteract()) return;

            _gameLoopManager.StartGame();
        }
    }
}