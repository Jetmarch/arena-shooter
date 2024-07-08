using Zenject;

namespace ArenaShooter.UI
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<HealthBarController>().AsSingle();
            Container.Bind<HordeScenarioActView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CapturePointScenarioActView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BossScenarioActView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<HordeScenarioActController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CapturePointScenarioActController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BossScenarioActController>().AsSingle().NonLazy();
        }
    }
}