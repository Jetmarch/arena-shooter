using ArenaShooter.Mechanics;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Projectiles
{
    public class ExplosionProjectileInstaller : MonoInstaller
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private ParticleSystem _explosionParticles;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SplashDamageMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<ProjectileImpactAfterDelayMechanic>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<ProjectileImpactAfterDelayController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileDestroyOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileDestroyOnHitMechanic>().AsSingle().WithArguments(_explosionParticles, _spriteRenderer, gameObject).NonLazy();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _explosionParticles = GetComponentInChildren<ParticleSystem>();
        }
#endif
    }
}