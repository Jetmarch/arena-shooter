using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using UnityEngine;

namespace ArenaShooter.Installers
{
    [RequireComponent(typeof(PlayerScannerComponent))]
    [RequireComponent(typeof(CapturePointComponent))]
    public class CapturePointInstaller : MonoBehaviour
    {
        [SerializeField]
        private CircleTrigger2DComponent _circleTrigger2DComponent;
        [SerializeField]
        private PlayerScannerComponent _playerScannerComponent;
        [SerializeField]
        private CapturePointComponent _capturePointComponent;

        public void Construct()
        {
            _circleTrigger2DComponent.Construct();
            _playerScannerComponent.Construct(_circleTrigger2DComponent);
            _capturePointComponent.Construct(_playerScannerComponent);
        }

        private void Start()
        {
            Construct();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _capturePointComponent = GetComponent<CapturePointComponent>();
            _circleTrigger2DComponent = GetComponent<CircleTrigger2DComponent>();
            _playerScannerComponent = GetComponent<PlayerScannerComponent>();
        }
#endif
    }
}