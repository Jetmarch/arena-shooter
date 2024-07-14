using UnityEngine;
using Zenject;

namespace ArenaShooter.UI
{
    public class MenuSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private string _gameSceneName = "ArenaScene_1";
        public override void InstallBindings()
        {
            Container.Bind<MenuView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ArmoryView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuController>().AsSingle().WithArguments(_gameSceneName).NonLazy();
            Container.BindInterfacesAndSelfTo<ArmoryController>().AsSingle().NonLazy();
        }
    }
}