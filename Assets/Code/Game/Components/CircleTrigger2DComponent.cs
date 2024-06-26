using UnityEngine;

namespace ArenaShooter.Components.Triggers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class CircleTrigger2DComponent : Trigger2DComponent
    {
        public void Construct()
        {
            _collider = GetComponent<CircleCollider2D>();
            _collider.isTrigger = true;
        }

        public void SetTriggerRadius(float radius)
        {
            var circle = _collider as CircleCollider2D;
            circle.radius = radius;
        }
    }
}