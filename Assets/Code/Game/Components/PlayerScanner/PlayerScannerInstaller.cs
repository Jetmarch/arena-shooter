using ArenaShooter.Components.Triggers;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Components
{
    public class PlayerScannerInstaller : MonoInstaller
    {
        [SerializeField]
        private CircleTrigger2DComponent _circleTriggerComponent;
        [SerializeField]
        private PlayerScannerComponent _scanner;
        public override void InstallBindings()
        {
            _circleTriggerComponent.Construct();

            Container.Bind<CircleTrigger2DComponent>().FromInstance(_circleTriggerComponent).AsSingle();
            Container.Bind<PlayerScannerComponent>().FromInstance(_scanner).AsSingle();

            Container.BindInterfacesAndSelfTo<CirclePlayerScannerController>().AsSingle().NonLazy();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _circleTriggerComponent = GetComponent<CircleTrigger2DComponent>();
            _scanner = GetComponent<PlayerScannerComponent>();
        }
#endif
    }
}