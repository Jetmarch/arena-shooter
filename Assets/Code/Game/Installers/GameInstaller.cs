using ArenaShooter.AI;
using ArenaShooter.CameraControllers;
using ArenaShooter.Inputs;
using ArenaShooter.Scenarios;
using ArenaShooter.Units;
using ArenaShooter.Units.Factories;
using ArenaShooter.Weapons;
using ArenaShooter.Weapons.Projectiles;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Installers
{
    /// <summary>
    /// TODO: Разбить инсталлеры на подсистемы
    /// </summary>
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
        private CameraMoveController _cameraMoveController;

        [SerializeField]
        private UnitManager _unitManager;

        [SerializeField]
        private ArenaScenarioConfiguration _arenaScenarioConfig;

        public override void InstallBindings()
        {

            Container.BindInstance(_currentInputController).AsSingle();
            Container.BindInstance(_stateMachineFactory).AsSingle();
            Container.BindInstance(_projectileFactory).AsSingle();
            Container.BindInstance(_weaponFactory).AsSingle();

            Container.BindInstance(_unitManager).AsSingle();

            BindInputController();

            BindUnitFactories();
            BindScenarioActExecutors();

            Container.BindInstance(_arenaScenarioConfig.GetScenarioActs()).AsSingle();
        }

        private void BindInputController()
        {
            Container.Bind(typeof(IMoveInputProvider), typeof(IScreenMouseMoveInputProvider), typeof(IWorldMouseMoveInputProvider),
                typeof(IShootInputProvider), typeof(IReloadInputProvider),
                typeof(IChangeWeaponInputProvider), typeof(IDashInputProvider)).FromInstance(_currentInputController);
        }

        private void BindUnitFactories()
        {
            var unitFactories = new List<BaseUnitFactory>();

            foreach(var unitFactory in FindObjectsOfType<BaseUnitFactory>())
            {
                unitFactories.Add(unitFactory);
            }

            Container.BindInstance(unitFactories).AsSingle();
        }

        private void BindScenarioActExecutors()
        {
            var scenarioActExecutors = new List<BaseScenarioActExecutor>();

            foreach(var actExecutor in FindObjectsOfType<BaseScenarioActExecutor>())
            {
                scenarioActExecutors.Add(actExecutor);
            }

            Container.BindInstance(scenarioActExecutors).AsSingle();
        }

        //Для тестирования
        public override void Start()
        {

            var player = _unitManager.CreateUnit(UnitType.Player, Vector3.zero, null);
            _cameraMoveController.SetTarget(player.transform);
            //_unitManager.CreateUnit(UnitType.EnemyShooter, new Vector3(5f, 5f, 0f), null);
            //_unitManager.CreateUnit(UnitType.EnemyShooter, new Vector3(-5f, -5f, 0f), null);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _stateMachineFactory = FindObjectOfType<AIStateMachineFactory>();
            _weaponFactory = FindObjectOfType<PlayerWeaponFactory>();
            _projectileFactory = FindObjectOfType<ProjectileFactory>();

            _cameraMoveController = FindObjectOfType<CameraMoveController>();
            _unitManager = FindObjectOfType<UnitManager>();
        }
#endif
    }
}