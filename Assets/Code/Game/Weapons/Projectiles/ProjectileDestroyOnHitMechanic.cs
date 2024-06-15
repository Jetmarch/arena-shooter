using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileDestroyOnHitMechanic : MonoBehaviour
    {
        private ProjectileDamageMechanic _damageController;

        public void Construct(ProjectileDamageMechanic damageController)
        {
            _damageController = damageController;
            _damageController.HitGameObject += OnHit;
        }

        private void OnEnable()
        {
            if (_damageController == null) return;

            _damageController.HitGameObject -= OnHit;
        }

        private void OnDisable()
        {
            _damageController.HitGameObject += OnHit;
        }

        private void OnHit(GameObject obj)
        {
            Destroy(gameObject);
        }
    }
}