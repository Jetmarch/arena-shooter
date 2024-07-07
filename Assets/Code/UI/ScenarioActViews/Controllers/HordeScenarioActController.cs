using ArenaShooter.Scenarios;
using Zenject;

namespace ArenaShooter.UI
{
    public class HordeScenarioActController : IInitializable, ILateDisposable
    {
        private HordeScenarioActView _view;
        private HordeScenarioActExecutor _executor;

        public HordeScenarioActController(HordeScenarioActView view, HordeScenarioActExecutor executor)
        {
            _view = view;
            _executor = executor;
        }

        public void Initialize()
        {
            _executor.ScenarioActStart += SetupView;
            _executor.ScenarioActFinish += _view.Hide;
            _executor.HordeUnitDied += UpdateView;
        }

        public void LateDispose()
        {
            _executor.ScenarioActStart -= SetupView;
            _executor.ScenarioActFinish -= _view.Hide;
            _executor.HordeUnitDied -= UpdateView;
        }

        private void SetupView()
        {
            _view.Setup(_executor.GetCountOfRemainingUnits(), _executor.GetCountOfUnitsInHorde());
        }

        private void UpdateView()
        {
            _view.UpdateProgress(_executor.GetCountOfRemainingUnits());
        }
    }
}