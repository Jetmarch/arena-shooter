using ArenaShooter.Scenarios;
using Zenject;

namespace ArenaShooter
{
    public class PauseGameOnVictoryController : IInitializable, ILateDisposable
    {
        private GameLoopManager _gameLoopManager;
        private ArenaScenarioExecutor _scenarioExecutor;

        public PauseGameOnVictoryController(GameLoopManager gameLoopManager, ArenaScenarioExecutor scenarioExecutor)
        {
            _gameLoopManager = gameLoopManager;
            _scenarioExecutor = scenarioExecutor;
        }

        public void Initialize()
        {
            _scenarioExecutor.OnScenarioFinish += _gameLoopManager.PauseGame;
        }

        public void LateDispose()
        {
            _scenarioExecutor.OnScenarioFinish -= _gameLoopManager.PauseGame;
        }
    }
}