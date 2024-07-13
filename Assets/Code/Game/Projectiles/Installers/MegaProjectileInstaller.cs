using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Mechanics;
using ArenaShooter.Weapons;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Projectiles
{
    public class MegaProjectileInstaller : MonoInstaller
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Move2DComponent _moveComponent;
        [SerializeField]
        private ParticleSystem _explosionParticles;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private int _countOfHitBeforeDestroy = 1;
        public override void InstallBindings()
        {
            _moveComponent.Construct(_rigidbody);

            Container.Bind<Trigger2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<SpreadProjectilesMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<DamageMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<DamageController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileSpreadOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileDestroyOnHitMechanic>().AsSingle().WithArguments(_explosionParticles, _spriteRenderer, gameObject, _countOfHitBeforeDestroy).NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileDestroyOnHitController>().AsSingle().NonLazy();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _moveComponent = GetComponent<Move2DComponent>();
            _explosionParticles = GetComponentInChildren<ParticleSystem>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
#endif
    }
}