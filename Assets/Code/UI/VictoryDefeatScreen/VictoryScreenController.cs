using ArenaShooter.Scenarios;
using Zenject;

namespace ArenaShooter.UI
{
    public class VictoryScreenController : IInitializable, ILateDisposable
    {
        private ArenaScenarioExecutor _scenarioExecutor;
        private AnnouncementScreenView _announcementScreenView;

        public VictoryScreenController(ArenaScenarioExecutor scenarioExecutor, AnnouncementScreenView announcementScreenView)
        {
            _scenarioExecutor = scenarioExecutor;
            _announcementScreenView = announcementScreenView;
        }

        public void Initialize()
        {
            _scenarioExecutor.OnScenarioFinish += OnScenarionFinish;
        }

        public void LateDispose()
        {
            _scenarioExecutor.OnScenarioFinish -= OnScenarionFinish;
        }

        private void OnScenarionFinish()
        {
            //TODO: Перенести в текст конфиги
            _announcementScreenView.ShowText("VICTORY");
        }
    }
}