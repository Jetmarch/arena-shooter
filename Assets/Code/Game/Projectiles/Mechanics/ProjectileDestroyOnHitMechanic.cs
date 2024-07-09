using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Projectiles
{
    public class ProjectileDestroyOnHitMechanic 
    {
        private ParticleSystem _explosionEffect;
        private SpriteRenderer _spriteRenderer;
        private GameObject _projectile;
        private bool _isDestroyed;

        public ProjectileDestroyOnHitMechanic(ParticleSystem explosionEffect, SpriteRenderer spriteRenderer, GameObject projectile)
        {
            _explosionEffect = explosionEffect;
            _spriteRenderer = spriteRenderer;
            _projectile = projectile;
            _isDestroyed = false;
        }

        public void OnHit(GameObject obj)
        {
            if (_isDestroyed) return;
            _spriteRenderer.enabled = false;
            _explosionEffect.Play();
            Object.Destroy(_projectile, _explosionEffect.main.duration);
            _isDestroyed = true;
        }
    }
}