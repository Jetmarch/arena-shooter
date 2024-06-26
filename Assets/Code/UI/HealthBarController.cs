using ArenaShooter.Units.Player;
using Zenject;

namespace ArenaShooter.UI
{
    public class HealthBarController : IInitializable, ILateDisposable
    {
        private PlayerFacade _player;
        private HealthBar _healthBar;

        public HealthBarController(IPlayerProvider playerProvider, HealthBar healthBar)
        {
            _player = playerProvider.Player;
            _healthBar = healthBar;
        }

        public void Initialize()
        {
            _player.HealthComponent.CurrentHealthChanged += _healthBar.UpdateHealth;
        }

        public void LateDispose()
        {
            _player.HealthComponent.CurrentHealthChanged -= _healthBar.UpdateHealth;
        }
    }
}