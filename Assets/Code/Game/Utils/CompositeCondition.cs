using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Utils
{
    public class CompositeCondition
    {
        private List<Func<bool>> _conditions;

        public CompositeCondition()
        {
            _conditions = new List<Func<bool>>();
        }

        public void Append(Func<bool> cond) => this._conditions.Add(cond);

        public bool IsTrue()
        {
            foreach (var cond in _conditions)
            {
                if (!cond.Invoke())
                    return false;
            }

            return true;
        }
    }
}