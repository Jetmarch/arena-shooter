using ArenaShooter.Components;
using System;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileDamageMechanic : MonoBehaviour, IProjectileDamageMechanic
    {
        [SerializeField]
        private float _minDamage;
        [SerializeField]
        private float _maxDamage;

        public event Action<GameObject> HitGameObject;

        public void OnHit(Collider2D obj)
        {
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