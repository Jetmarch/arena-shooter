using Zenject;

namespace ArenaShooter.UI
{
    public class MenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ArmoryView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ArmoryController>().AsSingle().NonLazy();
        }
    }
}