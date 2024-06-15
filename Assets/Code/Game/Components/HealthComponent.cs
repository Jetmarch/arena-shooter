using System;
using UnityEngine;

namespace ArenaShooter.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField]
        private float _maxHealth;
        [SerializeField] 
        private float _minHealth;
        [SerializeField]
        private float _currentHealth;

        public event Action<float> CurrentHealthChanged;
        public event Action<float> MaxHealthChanged;

        public float CurrentHealth {  get { return _currentHealth; } }
        public float MaxHealth { get { return _maxHealth; } }
        public float MinHealth { get { return _minHealth; } }

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void SetCurrentHealth(float health)
        {
            _currentHealth = health;
            CurrentHealthChanged?.Invoke(_currentHealth);
        }

        public void SetMaxHealth(float health)
        {
            _maxHealth = health;
            MaxHealthChanged?.Invoke(_maxHealth);
        }
    }
}