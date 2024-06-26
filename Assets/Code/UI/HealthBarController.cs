using ArenaShooter.Units.Player;
using UnityEngine;
using Zenject;

namespace ArenaShooter.UI
{
    public class HealthBarController : IInitializable, ILateDisposable
    {
        private PlayerFacade _player;
        private HealthBar _healthBar;

        public HealthBarController(PlayerFacade playerCreator, HealthBar healthBar)
        {
            _player = playerCreator;
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