using ArenaShooter.Components;
using System;
using UnityEngine;

namespace ArenaShooter.Units
{
    public class UnitDieMechanic : MonoBehaviour
    {
        private HealthComponent _healthComponent;


        public event Action<GameObject> OnDie;
        public bool IsDead { get { return _healthComponent.CurrentHealth > _healthComponent.MinHealth; } }

        public void Construct(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
            _healthComponent.CurrentHealthChanged += OnCurrentHealthChanged;
        }

        private void OnEnable()
        {
            if (_healthComponent == null) return;
            _healthComponent.CurrentHealthChanged += OnCurrentHealthChanged;
        }

        private void OnDisable()
        {
            if (_healthComponent == null) return;
            _healthComponent.CurrentHealthChanged -= OnCurrentHealthChanged;
        }

        private void OnCurrentHealthChanged(float health)
        {
            if (health <= _healthComponent.MinHealth)
            {
                Debug.Log($"{gameObject.name} is dead");
                OnDie?.Invoke(gameObject);

                //TODO: Временная штука
                Destroy(gameObject);
            }
        }

        public void Die()
        {
            OnDie?.Invoke(gameObject);
        }
    }
}