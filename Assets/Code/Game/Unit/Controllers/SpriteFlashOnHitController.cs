using ArenaShooter.Components;
using Zenject;

namespace ArenaShooter.Units.Enemies
{
    public class SpriteFlashOnHitController : IInitializable, ILateDisposable
    {
        private SpriteFlashMechanic _flashMechanic;
        private HealthComponent _healthComponent;

        public SpriteFlashOnHitController(SpriteFlashMechanic flashMechanic, HealthComponent healthComponent)
        {
            _flashMechanic = flashMechanic;
            _healthComponent = healthComponent;
        }

        public void Initialize()
        {
            _healthComponent.CurrentHealthChanged += Flash;
        }

        public void LateDispose()
        {
            _healthComponent.CurrentHealthChanged -= Flash;
        }

        private void Flash(float hp)
        {
            _flashMechanic.Flash();
        }
    }
}