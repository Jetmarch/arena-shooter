using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    public class UnitFootstepEffectMechanic : IInitializable, ILateDisposable, IGamePauseListener
    {
        private ParticleSystem _footstepEffect;
        private float _minVelocity;

        private bool _isPaused;

        public UnitFootstepEffectMechanic(ParticleSystem footstepEffect, float minVelocity = 0.1f)
        {
            _footstepEffect = footstepEffect;
            _minVelocity = minVelocity;
        }

        public void Initialize()
        {
            IGameLoopListener.Register(this);
        }

        public void LateDispose()
        {
            IGameLoopListener.Unregister(this);
        }

        public void OnPauseGame()
        {
            _isPaused = true;
        }

        public void OnResumeGame()
        {
            _isPaused = false;
        }

        public void PlayEffect(Vector2 moveVector)
        {
            if (_isPaused) return;

            if (moveVector.sqrMagnitude > 0.1f)
            {
                _footstepEffect.Play();
            }
        }
    }
}