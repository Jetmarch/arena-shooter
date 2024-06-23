using ArenaShooter.Components;
using ArenaShooter.Inputs;
using Zenject;

namespace ArenaShooter.Units.Player
{
    public class UnitMoveController : IInitializable, ILateDisposable
    {
        private IMoveInputProvider _moveInputProvider;
        private Move2DComponent _moveComponent;

        public UnitMoveController(IMoveInputProvider moveInputProvider, Move2DComponent moveMechanic)
        {
            _moveInputProvider = moveInputProvider;
            _moveComponent = moveMechanic;
        }

        public void Initialize()
        {
            _moveInputProvider.OnMove += _moveComponent.Move;
        }

        public void LateDispose()
        {
            _moveInputProvider.OnMove -= _moveComponent.Move;
        }
    }
}