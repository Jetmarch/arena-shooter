using ArenaShooter.Inputs;
using Zenject;

namespace ArenaShooter.CameraControllers
{
    public class CameraMouseMoveController : IInitializable, ILateDisposable
    {
        private IScreenMouseMoveInputProvider _mouseMoveProvider;
        private CameraMoveMechanic _cameraMoveComponent;

        public CameraMouseMoveController(IScreenMouseMoveInputProvider mouseMoveProvider, CameraMoveMechanic cameraMoveComponent)
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