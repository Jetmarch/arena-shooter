using ArenaShooter.Components;
using System;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileSplashDamageMechanic : MonoBehaviour, IProjectileDamageMechanic
    {
        [SerializeField]
        private float _damageRadius = 2f;

        [SerializeField]
        private float _damage = 2f;

        [SerializeField]
        private LayerMask _affectOnLayer;
        private GameObject _owner;
        public GameObject Owner { get => _owner; set => _owner = value; }

        public event Action<GameObject> HitGameObject;

        public void OnHit(Collider2D obj)
        {
            HitGameObject?.Invoke(obj.gameObject);

            var affectedTargets = Physics2D.CircleCastAll(obj.transform.position, _damageRadius, Vector2.up, float.MaxValue, _affectOnLayer);
            if (affectedTargets.Length <= 0) return;

            for (int i = 0; i < affectedTargets.Length; i++)
            {
                if (affectedTargets[i].transform.gameObject == _owner) continue;

                var health = affectedTargets[i].transform.gameObject.GetComponent<HealthComponent>();
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