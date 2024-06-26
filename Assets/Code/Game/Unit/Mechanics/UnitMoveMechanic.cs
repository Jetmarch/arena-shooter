using ArenaShooter.Components;
using ArenaShooter.Utils;
using System;
using UnityEngine;

namespace ArenaShooter.Units
{
    /// <summary>
    /// Позволяет передвигать юнита
    /// </summary>

    [Obsolete]
    [RequireComponent(typeof(Move2DComponent))]
    public sealed class UnitMoveMechanic : MonoBehaviour
    {
        private Move2DComponent _moveComponent;

        private CompositeCondition _condition;

        public CompositeCondition Condition { get { return _condition; } }

        public void Constuct(Move2DComponent moveComponent)
        {
            _moveComponent = moveComponent;
            _condition = new CompositeCondition();
        }

        public void OnMove(Vector2 moveVector)
        {
            if (!_condition.IsTrue()) return;

            _moveComponent.Move(moveVector);
        }
    }
}