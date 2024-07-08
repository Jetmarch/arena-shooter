using System;
using UnityEngine;

namespace ArenaShooter.Components
{
    public class CapturePointComponent : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField]
        private float _captureCurrentProgress = 0f;
        [SerializeField]
        private float _captureMaxProgress = 100f;

        [SerializeField]
        private float _captureSpeed = 2f;

        public event Action<float> CaptureProgress;
        public event Action PointActivated;
        public event Action PointDeactivated;
        public event Action PointCaptured;

        [SerializeField]
        private bool _isPlayerOnPoint;
        [SerializeField]
        private bool _isCaptureAvailable;

        public float CaptureCurrentProgress => _captureCurrentProgress;
        public float CaptureMaxProgress => _captureMaxProgress;

        public void OnPlayerEnterPoint(GameObject _)
        {
            Debug.Log("Player on the point!");
            _isPlayerOnPoint = true;
            IGameLoopListener.Register(this);
        }

        public void OnPlayerExitPoint(GameObject _)
        {
            Debug.Log("Player out");
            _isPlayerOnPoint = false;
            IGameLoopListener.Unregister(this);
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