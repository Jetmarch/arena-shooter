using ArenaShooter.Components;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ArenaShooter.AI
{
    public class MeleeAIBrain : AIBrain
    {
        [SerializeField]
        private float _chargeTime = 1.5f;

        [SerializeField]
        private float _delayBetweenCharges = 2f;

        [SerializeField]
        private bool _inCharge;

        [SerializeField]
        private bool _isChargeOnCooldown;

        [SerializeField]
        private float _radiusOfInteraction = 2f;

        [SerializeField]
        private bool _isWandering;

        [SerializeField]
        private float _chargeSpeed = 500f;

        [SerializeField]
        private float _wanderingSpeed = 50f;

        private Vector3 _chargeVelocity;

        private Vector3 _wanderingVelocity;

        public event Action<float> OnChangeMoveSpeed;

        protected override void UpdateAI()
        {
            if (_target == null) return;
            _inputController.WorldMouseMove(_target.position);
            _inputController.ScreenMouseMove(_target.position);

            if(_inCharge)
            {
                _inputController.Move(_chargeVelocity);
            }
            else if(_isChargeOnCooldown && !_isWandering)
            {
                var rndInCirclePosition = UnityEngine.Random.insideUnitCircle * _radiusOfInteraction;
                var positionNearTarget = new Vector3(_target.position.x + rndInCirclePosition.x, _target.position.y + rndInCirclePosition.y, 0f);
                _wanderingVelocity = (positionNearTarget - transform.position).normalized;

                _isWandering = true;
                //TODO: Подвязать к событию контроллер
                OnChangeMoveSpeed?.Invoke(_wanderingSpeed);
            }
            else if(_isWandering)
            {
                _inputController.Move(_wanderingVelocity);

                if (!_isChargeOnCooldown)
                {
                    _isWandering = false;
                    OnChangeMoveSpeed?.Invoke(_chargeSpeed);
                    PrepareToCharge();
                }
            }
            else
            {
                PrepareToCharge();
            }
        }

        private void PrepareToCharge()
        {
            _inputController.Move(Vector2.zero);
            _chargeVelocity = (_target.position - transform.position).normalized;
            StartCoroutine(Charge());
        }

        private IEnumerator Charge()
        {
            _inCharge = true;
            yield return new WaitForSeconds(_chargeTime);
            _inCharge = false;
            _isChargeOnCooldown = true;
            yield return new WaitForSeconds(_delayBetweenCharges);
            _isChargeOnCooldown = false;
        }
    }
}