using ArenaShooter.Components;
using ArenaShooter.Scenarios;
using Zenject;

namespace ArenaShooter.UI
{
    public class BossScenarioActController : IInitializable, ILateDisposable
    {
        private BossScenarioActView _view;
        private BossScenarioActExecutor _executor;
        private HealthComponent _healthComponent;

        public BossScenarioActController(BossScenarioActView view, BossScenarioActExecutor executor)
        {
            _view = view;
            _executor = executor;
        }

        public void Initialize()
        {
            _executor.ScenarioActStart += SetupView;
            _executor.ScenarioActFinish += _view.Hide;
        }

        public void LateDispose()
        {
            _executor.ScenarioActStart -= SetupView;
            _executor.ScenarioActFinish -= _view.Hide;

            if (_healthComponent == null) return;
            _healthComponent.CurrentHealthChanged -= _view.UpdateProgress;
        }

        private void SetupView()
        {
            _healthComponent = _executor.BossUnit.GetComponent<HealthComponent>();
            _view.Setup(_executor.BossUnit.name, _healthComponent.CurrentHealth, _healthComponent.MaxHealth);
            _healthComponent.CurrentHealthChanged += _view.UpdateProgress;
        }
    }
}