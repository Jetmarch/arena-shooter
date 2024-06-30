using ArenaShooter.Components.Triggers;
using ArenaShooter.Weapons.Projectiles;
using UnityEngine;
using Zenject;

public class ExplosionProjectileInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Trigger2DComponent>().FromComponentOn(gameObject).AsSingle();
        Container.BindInterfacesAndSelfTo<ProjectileSplashDamageMechanic>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<ProjectileDestroyOnHitMechanic>().FromComponentOn(gameObject).AsSingle();

        Container.BindInterfacesAndSelfTo<ProjectileDamageController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ProjectileDestroyOnHitController>().AsSingle().NonLazy();
    }
}