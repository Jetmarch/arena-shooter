using ArenaShooter.Mechanics;
using Zenject;

namespace ArenaShooter.Projectiles
{
    public class ExplosionProjectileInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SplashDamageMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<ProjectileImpactAfterDelayMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<ProjectileDestroyOnHitMechanic>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<ProjectileImpactAfterDelayController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileDestroyOnHitController>().AsSingle().NonLazy();
        }
    }
}