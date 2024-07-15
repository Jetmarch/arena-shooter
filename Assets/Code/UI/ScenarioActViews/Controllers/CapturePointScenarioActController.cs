using ArenaShooter.Scenarios;
using UnityEngine;
using Zenject;

namespace ArenaShooter.UI
{
    public class CapturePointScenarioActController : IInitializable, ILateDisposable
    {
        private CapturePointScenarioActView _view;
        private CapturePointScenarioActExecutor _executor;
        private Camera _camera;

        public CapturePointScenarioActController(CapturePointScenarioActView view, CapturePointScenarioActExecutor executor, Camera camera)
        {
            _view = view;
            _executor = executor;
            _camera = camera;
        }

        public void Initialize()
        {
            _executor.ScenarioActStart += SetupView;
            _executor.ScenarioActFinish += _view.Hide;
            _executor.CurrentCapturePoint.CaptureProgress += _view.UpdateProgress;
        }

        public void LateDispose()
        {
            _executor.ScenarioActStart -= SetupView;
            _executor.ScenarioActFinish -= _view.Hide;
            _executor.CurrentCapturePoint.CaptureProgress -= _view.UpdateProgress;
        }

        private void SetupView()
        {
            _view.Setup(_executor.CurrentCapturePoint.CaptureCurrentProgress,
                _executor.CurrentCapturePoint.CaptureMaxProgress,
                _executor.CurrentCapturePoint.transform,
                _camera);
        }
    }
}