using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.AI
{
    /// <summary>
    /// Создает машину состояний под указанный тип противника
    /// </summary>
    public class AIStateMachineFactory : MonoBehaviour
    {
        [SerializeField]
        private List<AIStateMachineContainer> _containers;

        public StateMachine CreateStateMachine(AIType type, AIInputController inputController, Transform owner)
        {
            var aiContainer = _containers.Find(x => x.Type == type);
            var stateMachine = new StateMachine();

            var idleState = new IdleState(owner, aiContainer);
            var pursuitState = new PursuitState(owner, aiContainer, inputController);
            var attackState = new AttackState(owner, aiContainer, inputController, this);

            var idleToPursuit = new StateTransition(pursuitState);
            idleToPursuit.Condition.Append(idleState.IsTargetInDistanceOfAggro);

            var pursuitToAttack = new StateTransition(attackState);
            pursuitToAttack.Condition.Append(pursuitState.IsTargetInDistanceOfAttack);

            var attackToPursuit = new StateTransition(pursuitState);
            attackToPursuit.Condition.Append(attackState.IsTargetNotInDistanceOfAttack);
            attackToPursuit.Condition.Append(attackState.IsNotAttack);

            var pursuitToIdle = new StateTransition(idleState);
            pursuitToIdle.Condition.Append(pursuitState.IsTargetNotInDistanceOfAggro);

            pursuitState.AddTransition(pursuitToAttack);
            attackState.AddTransition(attackToPursuit);

            idleState.AddTransition(idleToPursuit);

            stateMachine.AddState(idleState);
            stateMachine.AddState(pursuitState);
            stateMachine.AddState(attackState);
            stateMachine.SetCurrentState(idleState);

            return stateMachine;
        }
    }
}