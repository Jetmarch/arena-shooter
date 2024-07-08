using ArenaShooter.Mechanics;
using Zenject;

namespace ArenaShooter.UI
{
    public class StartScenarioInteractableController : IInitializable, ILateDisposable
    {
        private StartScenarioInteractable _interactable;
        private StartScenarioInteractableView _view;

        public StartScenarioInteractableController(StartScenarioInteractable interactable, StartScenarioInteractableView view)
        {
            _interactable = interactable;
            _view = view;
        }

        public void Initialize()
        {
            _interactable.OnActivate += _view.Show;
            _interactable.OnDeactivate += _view.Hide;
        }

        public void LateDispose()
        {
            _interactable.OnActivate -= _view.Show;
            _interactable.OnDeactivate -= _view.Hide;
        }
    }
}