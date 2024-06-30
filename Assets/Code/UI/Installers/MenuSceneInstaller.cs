using Zenject;

namespace ArenaShooter.UI
{
    public class MenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<MenuController>().AsSingle().NonLazy();
        }
    }
}