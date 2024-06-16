using ArenaShooter.Inputs;
using UnityEngine;

namespace ArenaShooter.AI
{
    public class PursuitState : BaseState
    {
        private Transform _owner;
        private Transform _target;
        private AIStateMachineContainer _container;
        private AIInputController _inputController;

        public PursuitState(Transform owner,  AIStateMachineContainer container, AIInputController inputController)
        {
            _owner = owner;
            _container = container;
            _inputController = inputController;
        }

        public override void Update()
        {
            //Debug.Log("Pursuit");
            var desiredVelocity = (_target.position - _owner.position).normalized;

            _inputController.Move(desiredVelocity);
        }

        public bool IsTargetInDistanceOfAttack()
        {
            var distance = Vector2.Distance(_owner.position, _target.position);
            return distance < _container.DistanceOfAttack;
        }

        public bool IsTargetNotInDistanceOfAggro()
        {
            var distance = Vector2.Distance(_owner.position, _target.position);
            return distance > _container.DistanceOfAggro;
        }
    }
}
