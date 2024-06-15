using ArenaShooter.Components;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileInstaller : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Move2DComponent _moveComponent;
        [SerializeField]
        private Trigger2DComponent _trigger2DComponent;
        [SerializeField]
        private ProjectileMoveMechanic _moveMechanic;
        [SerializeField]
        private ProjectileDamageMechanic _damageMechanic;
        [SerializeField]
        private ProjectileDestroyOnHitMechanic _destroyOnHitMechanic;

        public void Construct()
        {
            _moveComponent.Construct(_rigidbody);
            _moveMechanic.Construct(_moveComponent);
            _damageMechanic.Construct(_trigger2DComponent);
            _destroyOnHitMechanic.Construct(_damageMechanic);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _moveComponent = GetComponent<Move2DComponent>();
            _trigger2DComponent = GetComponent<Trigger2DComponent>();
            _moveMechanic = GetComponent<ProjectileMoveMechanic>();
            _damageMechanic = GetComponent<ProjectileDamageMechanic>();
            _destroyOnHitMechanic = GetComponent<ProjectileDestroyOnHitMechanic>();
        }
#endif
    }
}