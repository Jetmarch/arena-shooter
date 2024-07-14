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

        [SerializeField]
        private string _defeatText = "DEFEAT";
        [SerializeField]
        private string _victoryText = "VICTORY";

        public override void InstallBindings()
        {
            Container.Bind<UIPrefabContainer>().FromInstance(_playerUIPrefabContainer).AsSingle();
            Container.Bind<HealthBarController>().AsSingle();
            Container.Bind<HordeScenarioActView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CapturePointScenarioActView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BossScenarioActView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<StartScenarioInteractableView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<VictoryScreenView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<DefeatScreenView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameMenuView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ScorePointsView>().FromComponentInHierarchy().AsSingle();

            Container.Bind<SceneNameProvider>().FromInstance(_sceneNameProvider).AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerUICreateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DefeatScreenController>().AsSingle().WithArguments(_defeatText).NonLazy();
            Container.BindInterfacesAndSelfTo<VictoryScreenController>().AsSingle().WithArguments(_victoryText).NonLazy();
            Container.BindInterfacesAndSelfTo<HordeScenarioActController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CapturePointScenarioActController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BossScenarioActController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StartScenarioInteractableController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VictoryScreenButtonsController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DefeatScreenButtonsController>().AsSingle().NonLazy();
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
        private string _nextSceneName;

        public string MenuSceneName => _menuSceneName;
        public string NextSceneName => _nextSceneName;

        public SceneNameProvider(string menuSceneName, string nextSceneName)
        {
            _menuSceneName = menuSceneName;
            _nextSceneName = nextSceneName;
        }
    }
}