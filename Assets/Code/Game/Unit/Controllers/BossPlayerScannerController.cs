using ArenaShooter.AI;
using ArenaShooter.Components;
using Zenject;

namespace ArenaShooter.Units.Enemies
{
    public class BossPlayerScannerController : IInitializable, ILateDisposable
    {
        private BossBrain _bossBrain;
        private PlayerScannerComponent _playerScanner;

        public BossPlayerScannerController(BossBrain bossBrain, PlayerScannerComponent playerScanner)
        {
            _bossBrain = bossBrain;
            _playerScanner = playerScanner;
        }

        public void Initialize()
        {
            _playerScanner.OnPlayerDetected += _bossBrain.OnPlayerDetected;
            _playerScanner.OnPlayerLost += _bossBrain.OnPlayerLost;
        }

        public void LateDispose()
        {
            _playerScanner.OnPlayerDetected -= _bossBrain.OnPlayerDetected;
            _playerScanner.OnPlayerLost -= _bossBrain.OnPlayerLost;
        }
    }
}