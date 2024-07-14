using UnityEngine;


namespace ArenaShooter.Projectiles
{
    public class ProjectileFacade : MonoBehaviour
    {
        [SerializeField]
        private ProjectileDestroyOnHitMechanic _destroyOnHitMechanic;

        public ProjectileDestroyOnHitMechanic DestroyOnHitMechanic { get { return _destroyOnHitMechanic; } }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _destroyOnHitMechanic = GetComponent<ProjectileDestroyOnHitMechanic>();
        }
#endif
    }
}