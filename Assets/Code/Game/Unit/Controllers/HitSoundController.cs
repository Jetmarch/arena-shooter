
using ArenaShooter.Audio;
using ArenaShooter.Components;
using Zenject;

namespace ArenaShooter.Unit
{
    internal class HitSoundController : IInitializable, ILateDisposable
    {
        private AudioComponent _audioComponent;
        private HealthComponent _healthComponent;
        private string _hitSoundName;

        public HitSoundController(AudioComponent audioComponent, HealthComponent healthComponent, string hitSoundName)
        {
            _audioComponent = audioComponent;
            _healthComponent = healthComponent;
            _hitSoundName = hitSoundName;
        }

        public void Initialize()
        {
            _healthComponent.CurrentHealthChanged += PlayHitSound;
        }

        public void LateDispose()
        {
            _healthComponent.CurrentHealthChanged += PlayHitSound;
        }

        private void PlayHitSound(float _)
        {
            _audioComponent.Play(_hitSoundName);
        }
    }
}
