using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Components
{
    public class CapturePointController : IInitializable, ILateDisposable
    {
        private CapturePointComponent _capturePoint;
        private PlayerScannerComponent _playerScanner;

        public CapturePointController(CapturePointComponent capturePoint, PlayerScannerComponent playerScanner)
        {
            _capturePoint = capturePoint;
            _playerScanner = playerScanner;
        }

        public void Initialize()
        {
            _playerScanner.OnPlayerDetected += _capturePoint.OnPlayerEnterPoint;
            _playerScanner.OnPlayerLost += _capturePoint.OnPlayerExitPoint;
        }

        public void LateDispose()
        {
            _playerScanner.OnPlayerDetected -= _capturePoint.OnPlayerEnterPoint;
            _playerScanner.OnPlayerLost -= _capturePoint.OnPlayerExitPoint;
        }
    }
}