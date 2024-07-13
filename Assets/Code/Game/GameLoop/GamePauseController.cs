using ArenaShooter.Inputs;
using Zenject;

namespace ArenaShooter
{
    public class GamePauseController : IInitializable, ILateDisposable
    {
        private GameLoopManager _gameLoopManager;
        private IMenuInputProvider _inputProvider;

        public GamePauseController(GameLoopManager gameLoopManager, IMenuInputProvider inputProvider)
        {
            _gameLoopManager = gameLoopManager;
            _inputProvider = inputProvider;
        }

        public void Initialize()
        {
            _inputProvider.OnMenu += _gameLoopManager.ToggleGame;
        }

        public void LateDispose()
        {
            _inputProvider.OnMenu -= _gameLoopManager.ToggleGame;
        }
    }
}