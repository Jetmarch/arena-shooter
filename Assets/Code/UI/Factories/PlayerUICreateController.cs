using ArenaShooter.Units.Player;
using Zenject;

namespace ArenaShooter.UI
{
    public class PlayerUICreateController : IInitializable, ILateDisposable
    {
        private UIPrefabContainer _prefabContainer;

        private IPlayerProvider _playerProvider;
        private DiContainer _container;

        public PlayerUICreateController(UIPrefabContainer prefabContainer, IPlayerProvider playerProvider, DiContainer container)
        {
            _prefabContainer = prefabContainer;
            _playerProvider = playerProvider;
            _container = container;
        }

        public void Initialize()
        {
            _playerProvider.OnPlayerCreated += OnPlayerCreated;
        }

        public void LateDispose()
        {
            _playerProvider.OnPlayerCreated -= OnPlayerCreated;
        }

        private void OnPlayerCreated(PlayerFacade obj)
        {
            _container.InstantiatePrefab(_prefabContainer.UIPrefab, _prefabContainer.UIParent);
        }
    }
}