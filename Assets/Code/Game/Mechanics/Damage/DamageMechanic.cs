using ArenaShooter.Components;
using System;
using UnityEngine;

namespace ArenaShooter.Mechanics
{
    public class DamageMechanic : MonoBehaviour, IDamageMechanic
    {
        [SerializeField]
        private float _minDamage;
        [SerializeField]
        private float _maxDamage;

        private GameObject _owner;

        public event Action<GameObject> HitGameObject;
        public GameObject Owner { get => _owner; set => _owner = value; }

        public void OnHit(GameObject obj)
        {
            if (obj == null) return;
            if (obj.gameObject == _owner) return;

            HitGameObject?.Invoke(obj.gameObject);

            var health = obj.gameObject.GetComponent<HealthComponent>();
            if (health != null)
            {
                var damage = UnityEngine.Random.Range(_minDamage, _maxDamage);
                var currentHealthAfterDamage = health.CurrentHealth - damage;
                health.SetCurrentHealth(currentHealthAfterDamage);
            }
        }
    }
}