using ArenaShooter.Components;
using Zenject;

namespace ArenaShooter.CameraScripts
{
    public class CameraShakeOnPlayerHitController : IInitializable, ILateDisposable
    {
        private CameraShakeMechanic _shakeMechanic;
        private HealthComponent _healthComponent;
        private CameraShakeData _shakeData;

        public CameraShakeOnPlayerHitController(CameraShakeMechanic shakeMechanic, HealthComponent healthComponent, CameraShakeData shakeData)
        {
            _shakeMechanic = shakeMechanic;
            _healthComponent = healthComponent;
            _shakeData = shakeData;
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
            _shakeMechanic.ShakeCamera(_shakeData);
        }
    }
}