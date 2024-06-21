using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
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
        private BaseProjectileDamageMechanic _damageMechanic;
        [SerializeField]
        private ProjectileDestroyOnHitMechanic _destroyOnHitMechanic;

        public void Construct()
        {
            _moveComponent.Construct(_rigidbody);
            _moveMechanic.Construct(_moveComponent);

            _trigger2DComponent.TriggerOn += _damageMechanic.OnHit;
            _damageMechanic.HitGameObject += _destroyOnHitMechanic.OnHit;
        }

        private void OnEnable()
        {
            _trigger2DComponent.TriggerOn += _damageMechanic.OnHit;
            _damageMechanic.HitGameObject += _destroyOnHitMechanic.OnHit;
        }

        private void OnDisable()
        {
            _trigger2DComponent.TriggerOn -= _damageMechanic.OnHit;
            _damageMechanic.HitGameObject -= _destroyOnHitMechanic.OnHit;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _moveComponent = GetComponent<Move2DComponent>();
            _trigger2DComponent = GetComponent<Trigger2DComponent>();
            _moveMechanic = GetComponent<ProjectileMoveMechanic>();
            _damageMechanic = GetComponent<BaseProjectileDamageMechanic>();
            _destroyOnHitMechanic = GetComponent<ProjectileDestroyOnHitMechanic>();
        }
#endif
    }
}