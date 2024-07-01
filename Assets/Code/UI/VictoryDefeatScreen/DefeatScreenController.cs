using ArenaShooter.Units.Player;
using Zenject;

namespace ArenaShooter.UI
{
    public class DefeatScreenController : IInitializable, ILateDisposable
    {
        private IPlayerProvider _playerProvider;
        private AnnouncementScreenView _announcementScreenView;

        public DefeatScreenController(IPlayerProvider playerProvider, AnnouncementScreenView announcementScreenView)
        {
            _playerProvider = playerProvider;
            _announcementScreenView = announcementScreenView;
        }

        public void Initialize()
        {
            _playerProvider.OnPlayerDied += OnPlayerDied;
        }

        public void LateDispose()
        {
            _playerProvider.OnPlayerDied -= OnPlayerDied;
        }

        private void OnPlayerDied(PlayerFacade obj)
        {
            //TODO: Перенести в текст конфиги
            _announcementScreenView.ShowText("DEFEAT");
        }
    }
}