using ArenaShooter.Inputs;
using System.Collections;
using UnityEngine;

namespace ArenaShooter.AI
{
    public class AttackState : BaseState
    {
        private Transform _owner;
        private Transform _target;
        private AIStateMachineContainer _container;
        private AIInputController _inputController;
        private bool _isAttacking;
        private MonoBehaviour _coroutineStarter;

        public AttackState(Transform owner, Transform target, AIStateMachineContainer container, AIInputController inputController, MonoBehaviour coroutineStarter)
        {
            _owner = owner;
            _target = target;
            _container = container;
            _inputController = inputController;
            _coroutineStarter = coroutineStarter;
        }

        public override void Update()
        {
            //Debug.Log("Attack!");
            if (_isAttacking) return;

            _coroutineStarter.StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            //Debug.Log("Attack!");
            _isAttacking = true;
            _inputController.Move(Vector2.zero);

            for (int i = 0; i < _container.AttackCount; i++)
            {
                //Debug.Log("Shoot!");
                _inputController.Shoot();
                yield return new WaitForSeconds(_container.TimeBetweenAttacks);
            }

            _isAttacking = false;
        }

        public bool IsTargetNotInDistanceOfAttack()
        {
            var distance = Vector2.Distance(_owner.position, _target.position);
            return distance > _container.DistanceOfAttack;
        }

        public bool IsNotAttack()
        {
            return _isAttacking ? false : true;
        }
    }
}