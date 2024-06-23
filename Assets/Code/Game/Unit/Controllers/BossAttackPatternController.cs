using ArenaShooter.AI;
using Zenject;

namespace ArenaShooter.Units.Enemies
{
    public class BossAttackPatternController : IInitializable, ILateDisposable
    {
        private BossBrain _bossBrain;
        private BossAttackPattern _attackPattern;

        public BossAttackPatternController(BossBrain bossBrain, BossAttackPattern attackPattern)
        {
            _bossBrain = bossBrain;
            _attackPattern = attackPattern;
        }

        public void Initialize()
        {
            _bossBrain.OnAttack += _attackPattern.OnAttack;
        }

        public void LateDispose()
        {
            _bossBrain.OnAttack -= _attackPattern.OnAttack;
        }
    }
}