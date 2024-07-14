using ArenaShooter.Units.Player;
using Zenject;

namespace ArenaShooter.UI
{
    public class DefeatScreenController : IInitializable, ILateDisposable
    {
        private GameConditionManager _gameConditionManager;
        private DefeatScreenView _defeatScreenView;
        private string _defeatText;

        public DefeatScreenController(GameConditionManager gameConditionManager, DefeatScreenView defeatScreenView, string defeatText)
        {
            _gameConditionManager = gameConditionManager;
            _defeatScreenView = defeatScreenView;
            _defeatText = defeatText;
        }

        public void Initialize()
        {
            _gameConditionManager.OnDefeat += OnDefeat;
        }

        public void LateDispose()
        {
            _gameConditionManager.OnDefeat -= OnDefeat;
        }

        private void OnDefeat()
        {
            _defeatScreenView.Show(_defeatText);
        }
    }
}