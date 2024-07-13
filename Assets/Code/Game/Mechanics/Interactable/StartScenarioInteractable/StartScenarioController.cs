using ArenaShooter.Components;
using Zenject;

namespace ArenaShooter.Mechanics
{
    public class StartScenarioController : IInitializable, ILateDisposable
    {
        private PlayerScannerComponent _scanner;
        private StartScenarioInteractable _interactable;

        public StartScenarioController(PlayerScannerComponent scanner, StartScenarioInteractable interactable)
        {
            _scanner = scanner;
            _interactable = interactable;
        }

        public void Initialize()
        {
            _scanner.OnPlayerDetected += _interactable.Activate;
            _scanner.OnPlayerLost += _interactable.Deactivate;
        }

        public void LateDispose()
        {
            _scanner.OnPlayerDetected -= _interactable.Activate;
            _scanner.OnPlayerLost -= _interactable.Deactivate;
        }
    }
}