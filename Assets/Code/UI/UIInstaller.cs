using ArenaShooter.Mechanics;
using UnityEngine;
using Zenject;

namespace ArenaShooter.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private UIPrefabContainer _playerUIPrefabContainer;
        public override void InstallBindings()
        {
            Container.Bind<UIPrefabContainer>().FromInstance(_playerUIPrefabContainer).AsSingle();
            Container.Bind<HealthBarController>().AsSingle();
            Container.Bind<HordeScenarioActView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CapturePointScenarioActView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BossScenarioActView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<StartScenarioInteractableView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AnnouncementScreenView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerUICreateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DefeatScreenController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VictoryScreenController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HordeScenarioActController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CapturePointScenarioActController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BossScenarioActController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StartScenarioInteractableController>().AsSingle().NonLazy();
        }
    }
}