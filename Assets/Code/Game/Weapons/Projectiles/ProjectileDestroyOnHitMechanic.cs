using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileDestroyOnHitMechanic : MonoBehaviour
    {
        private ProjectileDamageMechanic _damageController;

        private void Start()
        {
            _damageController = GetComponent<ProjectileDamageMechanic>();
            _damageController.HitGameObject += OnHit;
        }

        private void OnHit(GameObject obj)
        {
            Destroy(gameObject);
        }
    }
}