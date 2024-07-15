using Zenject;

namespace ArenaShooter.UI
{
    public class VictoryScreenController : IInitializable, ILateDisposable
    {
        private VictoryScreenView _victoryScreenView;
        private GameConditionManager _gameConditionManager;
        private string _victoryText;

        public VictoryScreenController(GameConditionManager gameConditionManager, VictoryScreenView victoryScreenView, string victoryText)
        {
            _gameConditionManager = gameConditionManager;
            _victoryScreenView = victoryScreenView;
            _victoryText = victoryText;
        }

        public void Initialize()
        {
            _gameConditionManager.OnVictory += OnVictory;
        }

        public void LateDispose()
        {
            _gameConditionManager.OnVictory -= OnVictory;
        }

        private void OnVictory()
        {
            _victoryScreenView.Show(_victoryText);
        }
    }
}