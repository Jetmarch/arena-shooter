using ArenaShooter.Components;
using Zenject;

namespace ArenaShooter.CameraScripts
{
    public class CameraShakeOnPlayerHitController : IInitializable, ILateDisposable
    {
        private CameraShakeMechanic _shakeMechanic;
        private HealthComponent _healthComponent;
        private float _shakeDuration;

        public CameraShakeOnPlayerHitController(CameraShakeMechanic shakeMechanic, HealthComponent healthComponent, float shakeDuration)
        {
            _shakeMechanic = shakeMechanic;
            _healthComponent = healthComponent;
            _shakeDuration = shakeDuration;
        }

        public void Initialize()
        {
            _healthComponent.CurrentHealthChanged += OnPlayerHealthChanged;
        }

        public void LateDispose()
        {
            _healthComponent.CurrentHealthChanged -= OnPlayerHealthChanged;
        }

        private void OnPlayerHealthChanged(float health)
        {
            _shakeMechanic.ShakeCamera(_shakeDuration);
        }
    }
}