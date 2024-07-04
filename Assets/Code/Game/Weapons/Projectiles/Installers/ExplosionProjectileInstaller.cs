using ArenaShooter.Components.Triggers;
using Zenject;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ExplosionProjectileInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProjectileSplashDamageMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<ProjectileDestroyOnHitMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<ProjectileImpactAfterDelayMechanic>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<ProjectileImpactAfterDelayController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileDestroyOnHitController>().AsSingle().NonLazy();
        }
    }
}