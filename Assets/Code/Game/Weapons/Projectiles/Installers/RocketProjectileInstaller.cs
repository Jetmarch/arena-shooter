using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Mechanics;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons.Projectiles
{
    public class RocketProjectileInstaller : MonoInstaller
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Move2DComponent _moveComponent;
        public override void InstallBindings()
        {
            _moveComponent.Construct(_rigidbody);

            Container.Bind<Trigger2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<SplashDamageMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<ProjectileDestroyOnHitMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<DamageController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileDestroyOnHitController>().AsSingle().NonLazy();
        }

        //TODO: Временное решение
#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _moveComponent = GetComponent<Move2DComponent>();
        }
#endif
    }
}