using UnityEngine;

namespace ArenaShooter.AI
{
    public class IdleState : BaseState
    {
        private Transform _owner;
        private Transform _target;
        private AIStateMachineContainer _container;

        public IdleState(Transform owner, Transform target, AIStateMachineContainer container)
        {
            _owner = owner;
            _target = target;
            _container = container;
        }

        public override void Update()
        {
            //Debug.Log("Idle state here");
        }

        public bool IsTargetInDistanceOfAggro()
        {
            var distance = Vector2.Distance(_owner.position, _target.position);
            return distance < _container.DistanceOfAggro;
        }
    }
}