using ArenaShooter.Components;
using System;
using UnityEngine;

namespace ArenaShooter.Units
{
    public class UnitDieMechanic : MonoBehaviour
    {
        private HealthComponent _healthComponent;


        public event Action OnDie;
        public bool IsDead { get { return _healthComponent.CurrentHealth > _healthComponent.MinHealth; } }

        public void Construct(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }

        private void OnEnable()
        {
            _healthComponent.CurrentHealthChanged += OnCurrentHealthChanged;
        }

        private void OnDisable()
        {
            _healthComponent.CurrentHealthChanged -= OnCurrentHealthChanged;
        }

        private void OnCurrentHealthChanged(float health)
        {
            if (health <= _healthComponent.MinHealth)
            {
                Debug.Log($"{gameObject.name} is dead");
                OnDie?.Invoke();

                //TODO: Временная штука
                Destroy(gameObject);
            }
        }

        public bool IsNotDead()
        {
            return !IsDead;
        }
    }
}