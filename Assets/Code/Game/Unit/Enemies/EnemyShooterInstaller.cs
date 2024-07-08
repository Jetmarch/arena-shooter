using ArenaShooter.AI;
using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Inputs;
using ArenaShooter.Units.Player;
using ArenaShooter.Weapons;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(UnitTemporaryInvulnerableMechanic))]
    [RequireComponent(typeof(Move2DComponent))]
    [RequireComponent(typeof(AIInputController))]
    [RequireComponent(typeof(UnitDieMechanic))]
    public class EnemyShooterInstaller : MonoInstaller
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Move2DComponent _moveComponent;
        [SerializeField]
        private AIInputController _inputController;
        [SerializeField]
        private AIBrain _brain;

        [SerializeField]
        private CircleTrigger2DComponent _triggerComponent;
        [SerializeField]
        private PlayerScannerComponent _playerScanner;
        [SerializeField]
        private SpriteFlashMechanic _spriteFlashMechanic;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private HealthComponent _healthComponent;
        [SerializeField]
        private UnitTemporaryInvulnerableMechanic _unitTemporaryInvulnerableMechanic;
        [SerializeField]
        private WeaponsStorage _weaponStorage;
        [SerializeField]
        private WeaponChangeMechanic _weaponChangeMechanic;

        [SerializeField]
        private GameObject _weaponPrefab;
        [SerializeField]
        private Transform _weaponParent;

        public override void InstallBindings()
        {
            _healthComponent.Construct();
            _moveComponent.Construct(_rigidbody);
            _brain.Construct(_inputController, _playerScanner);
            _triggerComponent.Construct();
            _spriteFlashMechanic.Construct(_spriteRenderer);

            _weaponChangeMechanic.Construct(_weaponStorage);

            Container.Bind<UnitDieMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<HealthComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<UnitTemporaryInvulnerableMechanic>().FromComponentOn(gameObject).AsSingle();

            Container.BindInterfacesAndSelfTo<AIInputController>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<SpriteFlashMechanic>().FromInstance(_spriteFlashMechanic).AsSingle();
            Container.Bind<SpriteRenderer>().FromInstance(_spriteRenderer).AsSingle();

            Container.Bind<WeaponChangeMechanic>().FromInstance(_weaponChangeMechanic).AsSingle();

            Container.BindInterfacesAndSelfTo<WeaponRotateMechanic>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<UnitMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDieController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpriteFlashOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TemporaryInvulnerabilityOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<WeaponRotateController>().AsSingle().NonLazy();

            _healthComponent.Condition.Append(_unitTemporaryInvulnerableMechanic.IsNotInvulnerable);
        }

        private void Start()
        {
            _weaponStorage.AddWeapon(Container.InstantiatePrefab(_weaponPrefab, _weaponParent).GetComponent<WeaponFacade>());
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _moveComponent = GetComponent<Move2DComponent>();
            _inputController = GetComponent<AIInputController>();
            _brain = GetComponent<AIBrain>();
            _triggerComponent = GetComponentInChildren<CircleTrigger2DComponent>();
            _playerScanner = GetComponentInChildren<PlayerScannerComponent>();
            _spriteFlashMechanic = GetComponentInChildren<SpriteFlashMechanic>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _unitTemporaryInvulnerableMechanic = GetComponent<UnitTemporaryInvulnerableMechanic>();
            _healthComponent = GetComponentInChildren<HealthComponent>();
            _weaponStorage = GetComponent<WeaponsStorage>();
            _weaponChangeMechanic = GetComponent<WeaponChangeMechanic>();
        }
#endif
    }

}