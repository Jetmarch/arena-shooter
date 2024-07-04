using ArenaShooter.AI;
using ArenaShooter.Components;
using Zenject;

namespace ArenaShooter.Units
{
    public class EnemyMeleeChangeSpeedController : IInitializable, ILateDisposable
    {
        private MeleeAIBrain _meleeAIBrain;
        private Move2DComponent _moveComponent;

        public EnemyMeleeChangeSpeedController(MeleeAIBrain meleeAIBrain, Move2DComponent moveComponent)
        {
            _meleeAIBrain = meleeAIBrain;
            _moveComponent = moveComponent;
        }

        public void Initialize()
        {
            _meleeAIBrain.OnChangeMoveSpeed += OnChangeMoveSpeed;
        }
        public void LateDispose()
        {
            _meleeAIBrain.OnChangeMoveSpeed -= OnChangeMoveSpeed;
        }

        private void OnChangeMoveSpeed(float moveSpeed)
        {
            _moveComponent.MoveSpeed = moveSpeed;
        }
    }
}