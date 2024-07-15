using System;
using UnityEngine;

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