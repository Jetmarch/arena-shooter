using ArenaShooter.Audio;
using ArenaShooter.CameraControllers;
using ArenaShooter.Inputs;
using ArenaShooter.Scenarios;
using ArenaShooter.UI;
using ArenaShooter.Units;
using ArenaShooter.Units.Factories;
using ArenaShooter.Units.Player;
using ArenaShooter.Weapons;
using ArenaShooter.Projectiles;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Installers
{
    /// <summary>
    /// TODO: Разбить инсталлеры на подсистемы
    /// </summary>
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraMoveMechanic _cameraMoveMechanic;

        [SerializeField]
        private UnitManager _unitManager;

        [SerializeField]
        private ArenaScenarioConfiguration _arenaScenarioConfig;

        [SerializeField]
        private UIPrefabContainer _playerUIPrefabContainer;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ProjectileFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerWeaponFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CameraMoveMechanic>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<KeyboardAndMouseInputController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UnitFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle();

            BindScenarioActExecutors();

            Container.BindInstance(_arenaScenarioConfig.GetScenarioActs()).AsSingle();

            Container.BindInterfacesAndSelfTo<CameraMouseMoveController>().AsSingle().NonLazy();
            Container.Bind<ScorePointsStorage>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AddScorePointsOnEnemyDeathController>().AsSingle().NonLazy();
            Container.Bind<ArenaScenarioExecutor>().FromComponentInHierarchy().AsSingle();

            BindUI();
        }

        private void BindScenarioActExecutors()
        {
            var scenarioActExecutors = new List<BaseScenarioActExecutor>();

            foreach (var actExecutor in FindObjectsOfType<BaseScenarioActExecutor>())
            {
                scenarioActExecutors.Add(actExecutor);
            }

            Container.BindInstance(scenarioActExecutors).AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<UIPrefabContainer>().FromInstance(_playerUIPrefabContainer).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerUICreateController>().AsSingle().NonLazy();


            Container.Bind<AnnouncementScreenView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<DefeatScreenController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VictoryScreenController>().AsSingle().NonLazy();
        }

        //TODO: Убрать это
        public override void Start()
        {
            var player = _unitManager.CreateUnit(UnitType.Player, Vector3.zero, null);
            _cameraMoveMechanic.SetTarget(player.transform);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _cameraMoveMechanic = FindObjectOfType<CameraMoveMechanic>();
            _unitManager = FindObjectOfType<UnitManager>();
        }
#endif
    }
}