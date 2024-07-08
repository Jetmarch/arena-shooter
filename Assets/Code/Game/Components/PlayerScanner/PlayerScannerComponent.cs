using ArenaShooter.Components.Triggers;
using ArenaShooter.Units.Player;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(CircleTrigger2DComponent))]
    public class PlayerScannerComponent : MonoBehaviour
    {
        public event Action<GameObject> OnPlayerDetected;
        public event Action<GameObject> OnPlayerLost;

        public void OnScannerTriggerEnter(GameObject obj)
        {
            var player = obj.GetComponent<PlayerInstaller>();
            if (player == null) return;

            OnPlayerDetected?.Invoke(player.gameObject);
        }

        public void OnScannerTriggerExit(GameObject obj)
        {
            var player = obj.GetComponent<PlayerInstaller>();
            if (player == null) return;

            OnPlayerLost?.Invoke(player.gameObject);
        }
    }
}