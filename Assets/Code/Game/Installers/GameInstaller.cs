using ArenaShooter.AI;
using ArenaShooter.CameraControllers;
using ArenaShooter.Inputs;
using ArenaShooter.Units.Factories;
using ArenaShooter.Weapons;
using ArenaShooter.Weapons.Projectiles;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private AIStateMachineFactory _stateMachineFactory;

        [SerializeField]
        private KeyboardAndMouseInputController _currentInputController;

        [SerializeField]
        private ProjectileFactory _projectileFactory;

        [SerializeField]
        private PlayerWeaponFactory _weaponFactory;

        [SerializeField]
        private PlayerUnitFactory _playerUnitFactory;

        [SerializeField]
        private EnemyShooterUnitFactory _enemyShooterUnitFactory;

        [SerializeField]
        private CameraMoveController _cameraMoveController;

        public override void InstallBindings()
        {

            Container.BindInstance(_currentInputController).AsSingle();
            //Container.BindInstance(_player).AsSingle();
            Container.BindInstance(_stateMachineFactory).AsSingle();
            Container.BindInstance(_projectileFactory).AsSingle();
            Container.BindInstance(_weaponFactory).AsSingle();

            Container.Bind(typeof(IMoveInputProvider), typeof(IScreenMouseMoveInputProvider), typeof(IWorldMouseMoveInputProvider),
                typeof(IShootInputProvider), typeof(IReloadInputProvider),
                typeof(IChangeWeaponInputProvider), typeof(IDashInputProvider)).FromInstance(_currentInputController);
        }

        //��� ������������
        public override void Start()
        {
            var player = _playerUnitFactory.CreateUnit(Vector3.zero, null);
            _cameraMoveController.SetTarget(player.transform);

            _enemyShooterUnitFactory.CreateUnit(new Vector3(5f, 5f, 0f), null);
            _enemyShooterUnitFactory.CreateUnit(new Vector3(-5f, -5f, 0f), null);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _stateMachineFactory = FindObjectOfType<AIStateMachineFactory>();
            _weaponFactory = FindObjectOfType<PlayerWeaponFactory>();
            _projectileFactory = FindObjectOfType<ProjectileFactory>();

            //_player = FindObjectOfType<PlayerInstaller>().transform;
            _playerUnitFactory = FindObjectOfType<PlayerUnitFactory>();
            _enemyShooterUnitFactory = FindObjectOfType<EnemyShooterUnitFactory>();
            _cameraMoveController = FindObjectOfType<CameraMoveController>();
        }
#endif
    }
}