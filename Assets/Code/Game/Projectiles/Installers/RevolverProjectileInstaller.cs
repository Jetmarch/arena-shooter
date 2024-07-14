using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Mechanics;
using Zenject;

namespace ArenaShooter.Projectiles
{
    public class RevolverProjectileInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Trigger2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<DamageMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<ProjectileDestroyOnHitMechanic>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<DamageController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileDestroyOnHitController>().AsSingle().NonLazy();
        }

    }
}