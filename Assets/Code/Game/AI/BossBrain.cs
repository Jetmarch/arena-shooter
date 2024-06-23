using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using ArenaShooter.Weapons.Projectiles;
using ModestTree;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.AI
{
    public class BossBrain : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField]
        private float _radiusOfInteraction = 3f;
        [SerializeField]
        private float _maxMovePhaseTime = 3f;

        private bool _isAttackPhase = false;
        private bool _isMovePhase = false;

        private Vector3 _desiredPosition;
        private float _movePhaseTime;

        private Transform _target;

        public event Action OnAttack;
        public event Action<Vector2> OnMove;

        //TODO: Move it to BossInstaller
        private PlayerScannerComponent _scanner;
        private CircleTrigger2DComponent _circleTrigger;

        private Move2DComponent _moveComponent;
        private Rigidbody2D _rigidbody;

        private WeaponsStorage _weaponStorage;
        private WeaponChangeMechanic _weaponChangeMechanic;
        private BossAttackPattern _attackPattern;

        [Inject]
        private ProjectileFactory _projectileFactory;

        private void Start()
        {
            _circleTrigger = GetComponent<CircleTrigger2DComponent>();
            _scanner = GetComponent<PlayerScannerComponent>();
            _scanner.OnPlayerDetected += OnPlayerDetected;
            _scanner.OnPlayerLost += OnPlayerLost;
            _circleTrigger.Construct();
            _scanner.Construct(_circleTrigger);

            _rigidbody = GetComponent<Rigidbody2D>();
            _moveComponent = GetComponent<Move2DComponent>();
            _moveComponent.Construct(_rigidbody);
            _weaponStorage = GetComponent<WeaponsStorage>();
            _weaponChangeMechanic = GetComponent<WeaponChangeMechanic>();
            _weaponChangeMechanic.Construct(_weaponStorage);
            _attackPattern = GetComponent<BossAttackPattern>();
            _attackPattern.Construct(_weaponChangeMechanic);
            OnAttack += _attackPattern.OnAttack;

            foreach(var weapon in  _weaponStorage.Weapons)
            {
                weapon.GetComponent<BaseWeaponShootMechanic>().Construct(_projectileFactory);
            }
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }

        public void OnUpdate(float delta)
        {
            if (_target == null || _isAttackPhase)
            {
                _moveComponent.Move(Vector2.zero);
                return;
            }
            
            if (!_isMovePhase)
            {
                var rndInCirclePosition = UnityEngine.Random.insideUnitCircle * _radiusOfInteraction;
                var positionNearTarget = new Vector3(_target.position.x + rndInCirclePosition.x, _target.position.y + rndInCirclePosition.y, 0f);
                _desiredPosition = (positionNearTarget - transform.position).normalized;

                _isMovePhase = true;
                _movePhaseTime = 0f;
            }
            else if(_isMovePhase)
            {
                OnMove?.Invoke(_desiredPosition);
                Debug.Log("Move to target");
                _moveComponent.Move(_desiredPosition);
                _movePhaseTime += delta;
                if (Vector2.Distance(transform.position, _desiredPosition) < 0.1f || _movePhaseTime >= _maxMovePhaseTime)
                {
                    _isMovePhase = false;
                    OnAttack?.Invoke();
                    Debug.Log("Simple attack");
                    _moveComponent.Move(Vector2.zero);
                }
            }
            
        }

        public void OnPlayerDetected(GameObject target)
        {
            _target = target.transform;
        }

        public void OnPlayerLost(GameObject target)
        {
            _target = null;
        }

        public void OnAttackEnd()
        {
            _isAttackPhase = false;
        }
    }
}