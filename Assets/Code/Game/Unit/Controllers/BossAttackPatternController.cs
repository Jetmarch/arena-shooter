using ArenaShooter.AI;
using ArenaShooter.Inputs;
using Zenject;

namespace ArenaShooter.Units.Enemies
{
    public class BossAttackPatternController : IInitializable, ILateDisposable
    {
        private AIInputController _inputController;
        private BossAttackPattern _attackPattern;

        public BossAttackPatternController(AIInputController inputController, BossAttackPattern attackPattern)
        {
            _inputController = inputController;
            _attackPattern = attackPattern;
        }

        public void Initialize()
        {
            _inputController.OnShoot += _attackPattern.OnAttack;
        }

        public void LateDispose()
        {
            _inputController.OnShoot -= _attackPattern.OnAttack;
        }
    }
}