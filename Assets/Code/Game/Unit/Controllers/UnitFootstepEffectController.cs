using ArenaShooter.Inputs;
using Zenject;

namespace ArenaShooter.Units
{
    public class UnitFootstepEffectController : IInitializable, ILateDisposable
    {
        private UnitFootstepEffectMechanic _mechanic;
        private IMoveInputProvider _moveInputProvider;

        public UnitFootstepEffectController(UnitFootstepEffectMechanic footstepEffect, IMoveInputProvider moveInputProvider)
        {
            _mechanic = footstepEffect;
            _moveInputProvider = moveInputProvider;
        }

        public void Initialize()
        {
            _moveInputProvider.OnMove += _mechanic.PlayEffect;
        }

        public void LateDispose()
        {
            _moveInputProvider.OnMove -= _mechanic.PlayEffect;
        }
    }
}