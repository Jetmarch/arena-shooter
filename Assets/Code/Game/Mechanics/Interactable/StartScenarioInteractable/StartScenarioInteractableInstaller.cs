using ArenaShooter.Components;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Mechanics
{
    public class StartScenarioInteractableInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerScannerComponent _scanner;

        public override void InstallBindings()
        {
            Container.Bind<PlayerScannerComponent>().FromInstance(_scanner).AsSingle();

            //Container.BindInterfacesAndSelfTo<StartScenarioInteractable>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StartScenarioController>().AsSingle().NonLazy();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _scanner = GetComponentInChildren<PlayerScannerComponent>();
        }
#endif
    }
}