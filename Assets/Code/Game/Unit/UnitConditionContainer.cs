using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Units
{
    /// <summary>
    /// Контейнер с данными о состоянии юнита
    /// </summary>
    [Obsolete]
    public sealed class UnitConditionContainer : MonoBehaviour
    {
        //TODO: разделить на сущности
        [SerializeField]
        private float _baseSpeed = 250f;

        [SerializeField]
        private float _additionalSpeed = 0f;

        [SerializeField]
        private float _dashSpeed = 500f;

        [SerializeField]
        private float _dashTime = 0.25f;

        [SerializeField]
        private bool _isDashing = false;


        public float BaseSpeed { get { return _baseSpeed; } }
        public float AdditionalSpeed {  get { return _additionalSpeed; } set { _additionalSpeed = value; } }
        public float DashSpeed { get { return _dashSpeed; } }
        public float DashTime { get { return _dashTime; } }
        public bool IsDashing { get { return _isDashing; } set { _isDashing = value; } }
    }
}