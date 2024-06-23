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
    /// TODO: ������� ���������� �� ����������
    /// </summary>
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraMoveController _cameraMoveController;

        [SerializeField]
        private UnitManager _unitManager;

        [SerializeField]
        private ArenaScenarioConfiguration _arenaScenarioConfig;

        public override void InstallBindings()
        {
            Container.Bind<ProjectileFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerWeaponFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CameraMoveController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UnitManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<KeyboardAndMouseInputController>().FromComponentInHierarchy().AsSingle();


            BindUnitFactories();
            BindScenarioActExecutors();

            Container.BindInstance(_arenaScenarioConfig.GetScenarioActs()).AsSingle();
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

        //TODO: ������ ���
        public override void Start()
        {

            var player = _unitManager.CreateUnit(UnitType.Player, Vector3.zero, null);
            _cameraMoveController.SetTarget(player.transform);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _cameraMoveController = FindObjectOfType<CameraMoveController>();
            _unitManager = FindObjectOfType<UnitManager>();
        }
#endif
    }
}