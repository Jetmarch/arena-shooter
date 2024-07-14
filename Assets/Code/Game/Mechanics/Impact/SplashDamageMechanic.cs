using ArenaShooter.Components;
using System;
using UnityEngine;

namespace ArenaShooter.Mechanics
{
    public class SplashDamageMechanic : MonoBehaviour, IImpactMechanic
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

        public void OnHit(GameObject _)
        {
            var affectedTargets = Physics2D.CircleCastAll(transform.position, _damageRadius, Vector2.up, float.MaxValue, _affectOnLayer);
            if (affectedTargets.Length <= 0) return;

            for (int i = 0; i < affectedTargets.Length; i++)
            {
                if (affectedTargets[i].transform.gameObject == _owner) continue;

                HitGameObject?.Invoke(affectedTargets[i].transform.gameObject);

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