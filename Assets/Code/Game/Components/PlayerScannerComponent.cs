using ArenaShooter.Units.Player;
using System;
using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(Trigger2DComponent))]
    public class PlayerScannerComponent : MonoBehaviour
    {
        private Trigger2DComponent _trigger;

        public event Action<GameObject> OnPlayerDetected;
        public event Action<GameObject> OnPlayerLost;

        public void Construct(Trigger2DComponent trigger)
        {
            _trigger = trigger;

            _trigger.TriggerOn += OnScannerTriggerEnter;
            _trigger.TriggerOff += OnScannerTriggerExit;
        }

        private void OnScannerTriggerEnter(Collider2D obj)
        {
            var player = obj.GetComponent<PlayerInstaller>();
            if (player == null) return;

            OnPlayerDetected?.Invoke(player.gameObject);
        }

        private void OnScannerTriggerExit(Collider2D obj)
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
    }
}