using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileDestroyOnHitMechanic : MonoBehaviour
    {
        public void OnHit(GameObject obj)
        {
            Destroy(gameObject);
        }
    }
}