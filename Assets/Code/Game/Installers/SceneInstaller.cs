using ArenaShooter.Audio;
using ArenaShooter.CameraScripts;
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
using DG.Tweening;
using ArenaShooter.Mechanics;

namespace ArenaShooter.Installers
{
    /// <summary>
    /// TODO: ������� ���������� �� ����������
    /// </summary>
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraShakeData _shakeCameraDataOnEnemyDied;
        [SerializeField]
        private CameraFollowMechanic _cameraMoveMechanic;

        [SerializeField]
        private UnitManager _unitManager;

        [SerializeField]
        private ArenaScenarioConfiguration _arenaScenarioConfig;

        

        public override void InstallBindings()
        {
            BindCamera();

            Container.Bind<GameLoopManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ProjectileFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerWeaponFactory>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<KeyboardAndMouseInputController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UnitFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle();

            BindScenarioActExecutors();

            Container.BindInstance(_arenaScenarioConfig.GetScenarioActs()).AsSingle();

            Container.Bind<ScorePointsStorage>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AddScorePointsOnEnemyDeathController>().AsSingle().NonLazy();
            Container.Bind<ArenaScenarioExecutor>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<StartScenarioInteractable>().AsSingle().NonLazy();
        }

        private void BindCamera()
        {
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();

            Container.Bind<CameraFollowMechanic>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraMouseMoveController>().AsSingle().NonLazy();

            Container.Bind<CameraShakeMechanic>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraShakeOnEnemyDieController>().AsSingle().WithArguments(_shakeCameraDataOnEnemyDied).NonLazy();
        }

        private void BindScenarioActExecutors()
        {
            var scenarioActExecutors = new List<BaseScenarioActExecutor>();

            foreach (var actExecutor in FindObjectsOfType<BaseScenarioActExecutor>())
            {
                scenarioActExecutors.Add(actExecutor);
            }

            Container.BindInstance(scenarioActExecutors).AsSingle();

            Container.Bind<HordeScenarioActExecutor>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CapturePointScenarioActExecutor>().FromComponentInHierarchy().AsSingle();
            Container.Bind<BossScenarioActExecutor>().FromComponentInHierarchy().AsSingle();
        }

        //TODO: ������ ���
        public override void Start()
        {
            var player = _unitManager.CreateUnit(UnitType.Player, Vector3.zero, null);
            _cameraMoveMechanic.SetTarget(player.transform);

            DOTween.Init();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _cameraMoveMechanic = FindObjectOfType<CameraFollowMechanic>();
            _unitManager = FindObjectOfType<UnitManager>();
        }
#endif
    }
}