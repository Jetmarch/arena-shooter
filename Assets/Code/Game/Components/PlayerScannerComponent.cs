using ArenaShooter.Components.Triggers;
using ArenaShooter.Units.Player;
using System;
using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(CircleTrigger2DComponent))]
    public class PlayerScannerComponent : MonoBehaviour
    {
        [SerializeField]
        private float _scannerRadius = 10f;


        private CircleTrigger2DComponent _trigger;

        public event Action<GameObject> OnPlayerDetected;
        public event Action<GameObject> OnPlayerLost;

        public void Construct(CircleTrigger2DComponent trigger)
        {
            _trigger = trigger;

            _trigger.TriggerOn += OnScannerTriggerEnter;
            _trigger.TriggerOff += OnScannerTriggerExit;
            SetScannerRadius(_scannerRadius);
        }

        public void SetScannerRadius(float radius)
        {
            _trigger.SetTriggerRadius(radius);
        }

        private void OnScannerTriggerEnter(GameObject obj)
        {
            var player = obj.GetComponent<PlayerInstaller>();
            if (player == null) return;

            OnPlayerDetected?.Invoke(player.gameObject);
        }

        private void OnScannerTriggerExit(GameObject obj)
        {
            var player = obj.GetComponent<PlayerInstaller>();
            if (player == null) return;

            OnPlayerLost?.Invoke(player.gameObject);
        }

        private void OnEnable()
        {
            if (_trigger == null) return;

            _trigger.TriggerOn += OnScannerTriggerEnter;
            _trigger.TriggerOff += OnScannerTriggerExit;
        }

        private void OnDisable()
        {
            if (_trigger == null) return;

            _trigger.TriggerOn -= OnScannerTriggerEnter;
            _trigger.TriggerOff -= OnScannerTriggerExit;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.4f, 0.5f, 0.3f, 0.3f);
            Gizmos.DrawSphere(transform.position, _scannerRadius);
        }
#endif
    }
}