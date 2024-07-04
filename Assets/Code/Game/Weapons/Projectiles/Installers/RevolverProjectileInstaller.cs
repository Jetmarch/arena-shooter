using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Mechanics;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons.Projectiles
{
    public class RevolverProjectileInstaller : MonoInstaller
    {
        [SerializeField]
        private Rigidbody2D _rigidbody; //TODO: Помещать в контейнер конкретного GameObject
        [SerializeField]
        private Move2DComponent _moveComponent;

        public override void InstallBindings()
        {
            _moveComponent.Construct(_rigidbody);
            //_moveMechanic.Construct(_moveComponent);

            Container.Bind<Trigger2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<DamageMechanic>().FromComponentOn(gameObject).AsSingle();
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