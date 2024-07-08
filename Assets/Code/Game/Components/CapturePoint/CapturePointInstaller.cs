using ArenaShooter.Components;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Installers
{
    [RequireComponent(typeof(CapturePointComponent))]
    public class CapturePointInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerScannerComponent _playerScannerComponent;
        [SerializeField]
        private CapturePointComponent _capturePointComponent;


        public override void InstallBindings()
        {
            Container.Bind<PlayerScannerComponent>().FromInstance(_playerScannerComponent).AsSingle();
            Container.Bind<CapturePointComponent>().FromInstance(_capturePointComponent).AsSingle();

            Container.BindInterfacesAndSelfTo<CapturePointController>().AsSingle().NonLazy();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _capturePointComponent = GetComponent<CapturePointComponent>();
            _playerScannerComponent = GetComponentInChildren<PlayerScannerComponent>();
        }
#endif
    }
}