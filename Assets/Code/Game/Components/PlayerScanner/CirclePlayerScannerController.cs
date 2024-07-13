using ArenaShooter.Components.Triggers;
using Zenject;


namespace ArenaShooter.Components
{
    public class CirclePlayerScannerController : IInitializable, ILateDisposable
    {
        private CircleTrigger2DComponent _circleTrigger;
        private PlayerScannerComponent _playerScanner;

        public CirclePlayerScannerController(CircleTrigger2DComponent circleTrigger, PlayerScannerComponent playerScanner)
        {
            _circleTrigger = circleTrigger;
            _playerScanner = playerScanner;
        }

        public void Initialize()
        {
            _circleTrigger.TriggerOn += _playerScanner.OnScannerTriggerEnter;
            _circleTrigger.TriggerOff += _playerScanner.OnScannerTriggerExit;
        }

        public void LateDispose()
        {
            _circleTrigger.TriggerOn -= _playerScanner.OnScannerTriggerEnter;
            _circleTrigger.TriggerOff -= _playerScanner.OnScannerTriggerExit;
        }
    }
}