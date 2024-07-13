using UnityEngine;

namespace ArenaShooter.Projectiles
{
    public class ProjectileDestroyOnHitMechanic
    {
        private ParticleSystem _explosionEffect;
        private SpriteRenderer _spriteRenderer;
        private GameObject _projectile;
        private bool _isDestroyed;
        private int _countOfHitBeforeDestroy;

        public ProjectileDestroyOnHitMechanic(ParticleSystem explosionEffect, SpriteRenderer spriteRenderer, GameObject projectile, int countOfHitBeforeDestroy)
        {
            _explosionEffect = explosionEffect;
            _spriteRenderer = spriteRenderer;
            _projectile = projectile;
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
            Object.Destroy(_projectile, _explosionEffect.main.duration);
            _isDestroyed = true;
        }
    }
}