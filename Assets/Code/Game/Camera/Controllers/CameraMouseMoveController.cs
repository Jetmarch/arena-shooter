using ArenaShooter.Inputs;
using Zenject;

namespace ArenaShooter.CameraScripts
{
    public class CameraMouseMoveController : IInitializable, ILateDisposable
    {
        private IScreenMouseMoveInputProvider _mouseMoveProvider;
        private CameraFollowMechanic _cameraMoveComponent;

        public CameraMouseMoveController(IScreenMouseMoveInputProvider mouseMoveProvider, CameraFollowMechanic cameraMoveComponent)
        {
            _mouseMoveProvider = mouseMoveProvider;
            _cameraMoveComponent = cameraMoveComponent;
        }

        public void Initialize()
        {
            _mouseMoveProvider.OnScreenMouseMove += _cameraMoveComponent.MoveTo;
        }

        public void LateDispose()
        {
            _mouseMoveProvider.OnScreenMouseMove -= _cameraMoveComponent.MoveTo;
        }
    }
}