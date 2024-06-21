using ArenaShooter.Components;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileSplashDamageMechanic : BaseProjectileDamageMechanic
    {
        [SerializeField]
        private float _damageRadius = 2f;

        [SerializeField]
        private float _damage = 2f;

        [SerializeField]
        private LayerMask _affectOnLayer;

        protected override void OnHitMechanic(Collider2D obj)
        {
            var results = Physics2D.CircleCastAll(transform.position, _damageRadius, Vector2.up, float.MaxValue, _affectOnLayer);
            if (results.Length <= 0) return;

            for(int i = 0; i < results.Length; i++)
            {
                var health = results[i].transform.gameObject.GetComponent<HealthComponent>();
                if (health == null) continue;

                health.SetCurrentHealth(health.CurrentHealth - _damage);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.4f);
            Gizmos.DrawSphere(transform.position, _damageRadius);
        }
#endif
    }
}