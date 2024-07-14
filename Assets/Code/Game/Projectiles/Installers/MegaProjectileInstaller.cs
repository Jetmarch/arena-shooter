using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Mechanics;
using ArenaShooter.Weapons;
using Zenject;

namespace ArenaShooter.Projectiles
{
    public class MegaProjectileInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Trigger2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<SpreadProjectilesMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<DamageMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<ProjectileDestroyMechanic>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<ImpactController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileSpreadOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileDestroyOnHitController>().AsSingle().NonLazy();
        }
    }
}