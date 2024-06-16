using System;
using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(Collider2D))]
    public class Trigger2DComponent : MonoBehaviour
    {
        private Collider2D _collider;

        public event Action<Collider2D> TriggerOn;
        public event Action<Collider2D> TriggerOff;

        private void Start()
        {
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerOn?.Invoke(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            TriggerOff?.Invoke(collision);
        }
    }
}