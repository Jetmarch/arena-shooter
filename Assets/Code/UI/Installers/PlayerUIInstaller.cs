using UnityEngine;
using Zenject;

namespace ArenaShooter.UI
{
    public class PlayerUIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<HealthBarView>().FromComponentInHierarchy(this).AsSingle();
            Container.BindInterfacesAndSelfTo<HealthBarController>().AsSingle().NonLazy();
        }
    }
}