using ArenaShooter.Mechanics;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private UIPrefabContainer _playerUIPrefabContainer;

        [SerializeField]
        private SceneNameProvider _sceneNameProvider;
        public override void InstallBindings()
        {
            Container.Bind<UIPrefabContainer>().FromInstance(_playerUIPrefabContainer).AsSingle();
            Container.Bind<HealthBarController>().AsSingle();
            Container.Bind<HordeScenarioActView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CapturePointScenarioActView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BossScenarioActView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<StartScenarioInteractableView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AnnouncementScreenView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameMenuView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ScorePointsView>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<SceneNameProvider>().FromInstance(_sceneNameProvider).AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerUICreateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DefeatScreenController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VictoryScreenController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HordeScenarioActController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CapturePointScenarioActController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BossScenarioActController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StartScenarioInteractableController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AnnouncementScreenButtonsController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameMenuController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScorePointsController>().AsSingle().NonLazy();
        }
    }

    [Serializable]
    public class SceneNameProvider
    {
        [SerializeField]
        private string _menuSceneName;
        [SerializeField]
        private string _arenaSceneName;

        public string MenuSceneName => _menuSceneName;
        public string ArenaSceneName => _arenaSceneName;

        public SceneNameProvider(string menuSceneName, string arenaSceneName)
        {
            _menuSceneName = menuSceneName;
            _arenaSceneName = arenaSceneName;
        }
    }
}