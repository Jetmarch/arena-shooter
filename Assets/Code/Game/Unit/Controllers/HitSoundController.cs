
using ArenaShooter.Audio;
using ArenaShooter.Components;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Unit
{
    internal class HitSoundController : IInitializable, ILateDisposable
    {
        private AudioComponent _audioComponent;
        private HealthComponent _healthComponent;

        public HitSoundController(AudioComponent audioComponent, HealthComponent healthComponent)
        {
            _audioComponent = audioComponent;
            _healthComponent = healthComponent;
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
            _audioComponent.Play("hit");
        }
    }
}
