using ArenaShooter.Inputs;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    public class UnitFootstepEffectController : IInitializable, ILateDisposable
    {
        private ParticleSystem _footstepEffect;
        private IMoveInputProvider _moveInputProvider;

        public UnitFootstepEffectController(ParticleSystem footstepEffect, IMoveInputProvider moveInputProvider)
        {
            _footstepEffect = footstepEffect;
            _moveInputProvider = moveInputProvider;
        }

        public void Initialize()
        {
            _moveInputProvider.OnMove += PlayEffect;
        }

        public void LateDispose()
        {
            _moveInputProvider.OnMove -= PlayEffect;
        }

        private void PlayEffect(Vector2 moveVector)
        {
            if (moveVector.sqrMagnitude > 0.1f)
            {
                _footstepEffect.Play();
            }
        }
    }
}