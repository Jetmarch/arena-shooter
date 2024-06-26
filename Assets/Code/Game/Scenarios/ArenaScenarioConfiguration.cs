using ArenaShooter.Units.Factories;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Scenarios
{
    [CreateAssetMenu(fileName = "ArenaScenarioConfiguration", menuName = "SO/Configs/AreaScenarionConfiguration")]
    public class ArenaScenarioConfiguration : ScriptableObject
    {
        [SerializeField]
        private List<EditorScenarioActData> _scenarioActs = new();

        public List<EditorScenarioActData> EditorScenarioActs { get { return _scenarioActs; } }

        public List<BaseScenarioActData> GetScenarioActs()
        {
            //TODO: Преобразование между EditorScenarioActData в конкретный тип сценария
            var scenarioActs = new List<BaseScenarioActData>();
            foreach (var act in _scenarioActs)
            {
                switch (act.Type)
                {
                    case ScenarioType.Unknown:
                        continue;
                    case ScenarioType.Horde:
                        var hordeAct = new HordeScenarioActData(act.EnemyData);
                        scenarioActs.Add(hordeAct);
                        break;
                    case ScenarioType.CapturePoint:
                        var capturePoint = new CapturePointScenarioActData(act.TimeToCapture);
                        scenarioActs.Add(capturePoint);
                        break;
                    case ScenarioType.Boss:
                        var boss = new BossScenarioActData(act.BossPrefab);
                        scenarioActs.Add(boss);
                        break;
                }
            }

            return scenarioActs;
        }
    }

    /// <summary>
    /// Класс обертка для данных из разных видов сценариев
    /// </summary>
    [Serializable]
    public class EditorScenarioActData
    {
        [SerializeField]
        public ScenarioType Type;

        [SerializeField]
        public List<HordeEnemyData> EnemyData = new();

        [SerializeField]
        public float TimeToCapture;

        [SerializeField]
        public GameObject BossPrefab;
    }

    [Serializable]
    public enum ScenarioType
    {
        Unknown,
        Horde,
        CapturePoint,
        Boss
    }

    [Serializable]
    public class BaseScenarioActData
    {
        [SerializeField]
        protected ScenarioType _type;

        public ScenarioType Type { get { return _type; } set { _type = value; } }
    }

    [Serializable]
    public class HordeEnemyData
    {
        public UnitType UnitType;
        public int CountOfEnemies;
    }

    [Serializable]
    public class HordeScenarioActData : BaseScenarioActData
    {

        [SerializeField]
        private List<HordeEnemyData> _enemyData = new();
        public List<HordeEnemyData> EnemyData { get { return _enemyData; } }

        public HordeScenarioActData(List<HordeEnemyData> enemyData = null)
        {
            _type = ScenarioType.Horde;
            _enemyData = enemyData;
        }
    }

    [Serializable]
    public class CapturePointScenarioActData : BaseScenarioActData
    {
        [SerializeField]
        private float _timeToCapture;

        public float TimeToCapture { get { return _timeToCapture; } }

        public CapturePointScenarioActData(float timeToCapture = 0f)
        {
            _type = ScenarioType.CapturePoint;
            _timeToCapture = timeToCapture;
        }
    }

    [Serializable]
    public class BossScenarioActData : BaseScenarioActData
    {
        [SerializeField]
        private GameObject _bossPrefab;

        public GameObject BossPrefab { get { return _bossPrefab; } }

        public BossScenarioActData(GameObject bossPrefab = null)
        {
            _type = ScenarioType.Boss;
            _bossPrefab = bossPrefab;
        }
    }
}
