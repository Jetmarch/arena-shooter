using ArenaShooter.Components;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    public class UnitDieMechanic : MonoBehaviour
    {
        public event Action<GameObject> OnDie;

        private bool _isDead;
        public bool IsDead { get { return _isDead; } }

        public void OnCurrentHealthChanged(float health)
        {
            if (health <= 0)
            {
                _isDead = true;
                OnDie?.Invoke(gameObject);
                Debug.Log($"Object {gameObject.name} is dead");
                //TODO: Remove it
                Destroy(gameObject);
            }
        }

        public void Die()
        {
            OnDie?.Invoke(gameObject);
        }
    }
}