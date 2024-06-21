using ArenaShooter.Components;
using System;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileDamageMechanic : BaseProjectileDamageMechanic
    {
        [SerializeField]
        private float _minDamage;
        [SerializeField]
        private float _maxDamage;

        protected override void OnHitMechanic(Collider2D obj)
        {
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