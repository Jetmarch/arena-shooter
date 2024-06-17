using System;
using UnityEngine;

namespace ArenaShooter.AI
{
    [Serializable]
    public class AIStateMachineContainer
    {
        [SerializeField]
        private AIType _type;

        [SerializeField]
        private float _distanceOfAggro = 10f;

        [SerializeField]
        private float _distanceOfAttack = 4f;

        [SerializeField]
        private float _stopDistance = 1f;

        [SerializeField]
        private int _attackCount = 3;

        [SerializeField]
        private float _timeBetweenAttacks = 0.7f;

        public AIType Type { get { return _type; } }
        public float DistanceOfAggro { get { return _distanceOfAggro; } }
        public float DistanceOfAttack { get => _distanceOfAttack; }
        public float StopDistance { get => _stopDistance; }
        public int AttackCount { get => _attackCount; }
        public float TimeBetweenAttacks { get => _timeBetweenAttacks; }
    }
}