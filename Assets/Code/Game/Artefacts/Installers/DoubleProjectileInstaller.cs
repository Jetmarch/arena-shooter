using UnityEngine;
using Zenject;

namespace ArenaShooter.Artefacts
{
    public class DoubleProjectileInstaller : MonoInstaller
    {
        [SerializeField]
        private float _doubleProjectileChance = 1f;
        [SerializeField]
        private float _delayBetweenShots = 0.3f;
        public override void InstallBindings()
        {
            Container.Bind<DoubleProjectileMechanic>().AsSingle().WithArguments(_doubleProjectileChance, _delayBetweenShots, this).NonLazy();
            Container.BindInterfacesAndSelfTo<DoubleProjectileOnShootController>().AsSingle().NonLazy();
        }
    }
}