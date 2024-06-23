using ArenaShooter.Components.Triggers;
using ArenaShooter.Components;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class MegaProjectileInstaller : MonoBehaviour
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
        [SerializeField]
        private SpreadProjectilesMechanic _spreadProjectilesMechanic;

        public void Construct(ProjectileFactory projectileFactory)
        {
            _moveComponent.Construct(_rigidbody);
            _moveMechanic.Construct(_moveComponent);
            _spreadProjectilesMechanic.Construct(projectileFactory);

            _trigger2DComponent.TriggerOn += _damageMechanic.OnHit;
            _damageMechanic.HitGameObject += SpreadProjectileMechanicWrapper;
            _damageMechanic.HitGameObject += _destroyOnHitMechanic.OnHit;
        }

        private void SpreadProjectileMechanicWrapper(GameObject obj)
        {
            _spreadProjectilesMechanic.OnShoot();
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
            _spreadProjectilesMechanic = GetComponent<SpreadProjectilesMechanic>();
        }
#endif
    }
}