using Zenject;

namespace ArenaShooter.UI
{
    public class PlayerUIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<HealthBarView>().FromComponentInHierarchy(this).AsSingle();
            Container.Bind<AmmoView>().FromComponentInHierarchy(this).AsSingle();
            Container.BindInterfacesAndSelfTo<HealthBarController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AmmoController>().AsSingle().NonLazy();
        }
    }
}