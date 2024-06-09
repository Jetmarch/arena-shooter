using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileDestroyOnHitObserver : MonoBehaviour
    {
        private ProjectileDamageController _damageController;

        private void Start()
        {
            _damageController = GetComponent<ProjectileDamageController>();
            _damageController.HitGameObject += OnHit;
        }

        private void OnHit(GameObject obj)
        {
            Destroy(gameObject);
        }
    }
}