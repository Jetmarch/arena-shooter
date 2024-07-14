using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Mechanics;
using Zenject;

namespace ArenaShooter.Projectiles
{
    public class RocketProjectileInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Trigger2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<SplashDamageMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<ProjectileDestroyMechanic>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<ImpactController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileDestroyOnHitController>().AsSingle().NonLazy();
        }
    }
}