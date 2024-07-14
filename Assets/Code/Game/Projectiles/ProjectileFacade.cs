using ArenaShooter.Mechanics;
using UnityEngine;


namespace ArenaShooter.Projectiles
{
    public class ProjectileFacade : MonoBehaviour
    {
        [SerializeField]
        private ProjectileDestroyMechanic _destroyOnHitMechanic;
        public ProjectileDestroyMechanic DestroyOnHitMechanic { get { return _destroyOnHitMechanic; } }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _destroyOnHitMechanic = GetComponent<ProjectileDestroyMechanic>();
        }
#endif
    }
}