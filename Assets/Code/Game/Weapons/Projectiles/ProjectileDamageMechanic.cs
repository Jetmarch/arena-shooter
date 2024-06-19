using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using System;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    [RequireComponent(typeof(Trigger2DComponent))]
    public class ProjectileDamageMechanic : MonoBehaviour
    {
        [SerializeField]
        private float _minDamage;
        [SerializeField]
        private float _maxDamage;

        [SerializeField]
        private Trigger2DComponent _triggerComponent;

        public event Action<GameObject> HitGameObject;

        public void Construct(Trigger2DComponent triggerComponent)
        {
            _triggerComponent = triggerComponent;

            _triggerComponent.TriggerOn += OnHit;
        }

        private void OnEnable()
        {
            if (_triggerComponent == null) return;
            _triggerComponent.TriggerOn += OnHit;
        }

        private void OnDisable()
        {
            if (_triggerComponent == null) return;
            _triggerComponent.TriggerOn -= OnHit;
        }

        private void OnHit(Collider2D obj)
        {
            HitGameObject?.Invoke(obj.gameObject);

            var health = obj.gameObject.GetComponent<HealthComponent>();
            if (health != null)
            {
                //Отдельная механика?
                var damage = UnityEngine.Random.Range(_minDamage, _maxDamage);
                //Высчитывать урон, возможно, стоит внутри HealthComponent
                var currentHealthAfterDamage = health.CurrentHealth - damage;
                health.SetCurrentHealth(currentHealthAfterDamage);
                Debug.Log($"Hit unit {health.gameObject.name} on {damage} damage!");
            }
        }
    }
}