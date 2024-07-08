using ArenaShooter.Components;
using ArenaShooter.Units;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Scenarios
{
    public class BossScenarioActExecutor : BaseScenarioActExecutor
    {
        [SerializeField]
        private SpawnPointComponent _bossSpawnPoint;

        private UnitManager _unitManager;
        private BossScenarioActData _data;

        private GameObject _bossUnit;

        public GameObject BossUnit { get { return _bossUnit; } }

        [Inject]
        private void Construct(UnitManager unitManager)
        {
            _unitManager = unitManager;
            _scenarioType = ScenarioType.Boss;
        }

        public override void Execute(BaseScenarioActData data)
        {
            var bossData = data as BossScenarioActData;
            if (bossData == null)
            {
                throw new System.Exception($"Type mismatch between ScenarioType and ScenarioActData type!");
            }

            OnScenarioActStart();
            _data = bossData;

            _bossUnit = _unitManager.CreateUnit(Units.Factories.UnitType.Boss, _bossSpawnPoint.GetRandomPointInside(), null);
            var dieMechanic = _bossUnit.GetComponent<UnitDieMechanic>();
            if (dieMechanic == null)
            {
                throw new System.Exception("Boss doesn't have UnitDieMechanic. Fix it");
            }

            dieMechanic.OnDie += OnBossDie;
        }

        private void OnBossDie(GameObject obj)
        {
            OnScenarioActFinish();
        }
    }
}