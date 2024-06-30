using ArenaShooter.Components;
using ArenaShooter.Units.Player;
using Zenject;

namespace ArenaShooter.UI
{
    public class HealthBarController : IInitializable, ILateDisposable
    {
        private HealthComponent _healthComponent;
        private HealthBarView _healthBar;

        public HealthBarController(IPlayerProvider playerProvider, HealthBarView healthBar)
        {
            _healthComponent = playerProvider.Player.HealthComponent;
            _healthBar = healthBar;
        }

        public void Initialize()
        {
            _healthComponent.CurrentHealthChanged += _healthBar.UpdateHealth;
            _healthBar.UpdateHealth(_healthComponent.CurrentHealth);
        }

        public void LateDispose()
        {
            _healthComponent.CurrentHealthChanged -= _healthBar.UpdateHealth;
        }
    }
}