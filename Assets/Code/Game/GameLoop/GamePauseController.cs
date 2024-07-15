using ArenaShooter.Inputs;
using Zenject;

namespace ArenaShooter
{
    public class GamePauseController : IInitializable, ILateDisposable
    {
        private GameLoopManager _gameLoopManager;
        private IMenuInputProvider _inputProvider;
        private GameConditionManager _gameConditionManager;

        public GamePauseController(GameLoopManager gameLoopManager, IMenuInputProvider inputProvider, GameConditionManager gameConditionManager)
        {
            _gameLoopManager = gameLoopManager;
            _inputProvider = inputProvider;
            _gameConditionManager = gameConditionManager;
        }

        public void Initialize()
        {
            _inputProvider.OnMenu += ToggleGame;
        }

        public void LateDispose()
        {
            _inputProvider.OnMenu -= ToggleGame;
        }

        private void ToggleGame()
        {
            if (_gameConditionManager.GameCondition != GameCondition.Battle) return;
            _gameLoopManager.ToggleGame();
        }
    }
}