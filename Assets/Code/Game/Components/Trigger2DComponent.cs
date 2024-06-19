using System;
using UnityEngine;

namespace ArenaShooter.Components.Triggers
{
    [RequireComponent(typeof(Collider2D))]
    public class Trigger2DComponent : MonoBehaviour
    {
        protected Collider2D _collider;

        public event Action<Collider2D> TriggerOn;
        public event Action<Collider2D> TriggerOff;

        //TODO: Сделать метод Construct
        protected virtual void Start()
        {
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerOn?.Invoke(collision);
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            TriggerOff?.Invoke(collision);
        }
    }
}