using UnityEngine;

namespace ArenaShooter.Projectiles
{
    public class ProjectileDestroyMechanic : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _explosionEffect;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private bool _isDestroyed = false;
        [SerializeField]
        private int _countOfHitBeforeDestroy;

        public ProjectileDestroyMechanic(ParticleSystem explosionEffect, SpriteRenderer spriteRenderer, int countOfHitBeforeDestroy)
        {
            _explosionEffect = explosionEffect;
            _spriteRenderer = spriteRenderer;
            _isDestroyed = false;
            _countOfHitBeforeDestroy = countOfHitBeforeDestroy;
        }

        public void OnHit(GameObject obj)
        {
            _countOfHitBeforeDestroy--;
            if (_countOfHitBeforeDestroy > 0) return;

            if (_isDestroyed) return;
            _spriteRenderer.enabled = false;
            _explosionEffect.Play();
            Destroy(gameObject, _explosionEffect.main.duration);
            _isDestroyed = true;
        }

        public void SetCountOfHitBeforeDestroy(int countOfHitBeforeDestroy)
        {
            _countOfHitBeforeDestroy = countOfHitBeforeDestroy;
        }
    }
}