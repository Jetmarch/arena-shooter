using ArenaShooter.Scenarios;
using System.Collections;
using System.Collections.Generic;
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
            _executor.CurrentPoint.CaptureProgress += _view.UpdateProgress;
        }

        public void LateDispose()
        {
            _executor.ScenarioActStart -= SetupView;
            _executor.ScenarioActFinish -= _view.Hide;
            _executor.CurrentPoint.CaptureProgress -= _view.UpdateProgress;
        }

        private void SetupView()
        {
            _view.Setup(_executor.CurrentPoint.CaptureCurrentProgress,
                _executor.CurrentPoint.CaptureMaxProgress,
                _executor.CurrentPoint.transform,
                _camera);
        }
    }
}