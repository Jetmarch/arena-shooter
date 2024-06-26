using System;
using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(PlayerScannerComponent))]
    public class CapturePointComponent : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField]
        private float _captureCurrentProgress = 0f;
        [SerializeField]
        private float _captureMaxProgress = 100f;

        [SerializeField]
        private float _captureSpeed = 2f;

        private PlayerScannerComponent _playerScannerComponent;

        public event Action<float> CaptureProgress;
        public event Action PointActivated;
        public event Action PointDeactivated;
        public event Action PointCaptured;

        [SerializeField]
        private bool _isPlayerOnPoint;
        [SerializeField]
        private bool _isCaptureAvailable;

        public void Construct(PlayerScannerComponent playerScannerComponent)
        {
            _playerScannerComponent = playerScannerComponent;
            _playerScannerComponent.OnPlayerDetected += OnPlayerEnterPoint;
            _playerScannerComponent.OnPlayerLost += OnPlayerExitPoint;
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
            if (_playerScannerComponent == null) return;
            _playerScannerComponent.OnPlayerDetected += OnPlayerEnterPoint;
            _playerScannerComponent.OnPlayerLost += OnPlayerExitPoint;
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
            if (_playerScannerComponent == null) return;
            _playerScannerComponent.OnPlayerDetected -= OnPlayerEnterPoint;
            _playerScannerComponent.OnPlayerLost -= OnPlayerExitPoint;
        }

        private void OnPlayerEnterPoint(GameObject obj)
        {
            Debug.Log("Player on the point!");
            _isPlayerOnPoint = true;
        }

        private void OnPlayerExitPoint(GameObject obj)
        {
            Debug.Log("Player out");
            _isPlayerOnPoint = false;
        }

        public void ActivatePoint()
        {
            _isCaptureAvailable = true;
            PointActivated?.Invoke();
        }

        public void DeactivatePoint()
        {
            _isCaptureAvailable = false;
            PointDeactivated?.Invoke();
        }

        public void OnUpdate(float delta)
        {
            if (!_isCaptureAvailable) return;
            if (!_isPlayerOnPoint) return;

            _captureCurrentProgress += delta * _captureSpeed;
            CaptureProgress?.Invoke(_captureCurrentProgress);

            if (_captureCurrentProgress >= _captureMaxProgress)
            {
                PointCaptured?.Invoke();
                Debug.Log("Point captured!");
                DeactivatePoint();
            }
        }
    }
}