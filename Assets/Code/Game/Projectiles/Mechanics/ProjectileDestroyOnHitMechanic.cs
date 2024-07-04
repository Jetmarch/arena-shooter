using UnityEngine;

namespace ArenaShooter.Projectiles
{
    public class ProjectileDestroyOnHitMechanic : MonoBehaviour
    {
        public void OnHit(GameObject obj)
        {
            Destroy(gameObject);
        }
    }
}