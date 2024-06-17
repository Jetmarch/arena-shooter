using ArenaShooter.Scenarios;
using ArenaShooter.Units.Factories;
using UnityEditor;
using UnityEngine;

namespace ArenaShooter.Editor
{
    public class ArenaScenarioConfigurationWindow : EditorWindow
    {
        private ArenaScenarioConfiguration _selectedConfig;

        [MenuItem("Configs/Arena scenario config")]
        public static ArenaScenarioConfigurationWindow OpenWindow()
        {
            var window = GetWindow<ArenaScenarioConfigurationWindow>();
            return window;
        }

        private void OnGUI()
        {
            DrawHeader();
            if (_selectedConfig == null) return;

            DrawScenarioActs();
        }

        private void DrawHeader()
        {
            EditorGUILayout.BeginHorizontal("badge");
            _selectedConfig = EditorGUILayout.ObjectField("Arena config", _selectedConfig, typeof(ArenaScenarioConfiguration), false) as ArenaScenarioConfiguration;

            if(_selectedConfig == null)
            {
                EditorGUILayout.EndVertical();
                return;
            }

            if(GUILayout.Button("Save"))
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+ Horde"))
            {
                _selectedConfig.EditorScenarioActs.Add(new EditorScenarioActData() { Type = ScenarioType.Horde });
            }
            if (GUILayout.Button("+ CapturePoint"))
            {
                _selectedConfig.EditorScenarioActs.Add(new EditorScenarioActData() { Type = ScenarioType.CapturePoint });
            }
            if (GUILayout.Button("+ Boss"))
            {
                _selectedConfig.EditorScenarioActs.Add(new EditorScenarioActData() { Type = ScenarioType.Boss });
            }
            if (GUILayout.Button("-"))
            {
                if (_selectedConfig.EditorScenarioActs.Count == 0)
                {
                    EditorGUILayout.EndHorizontal();
                    return;
                }
                _selectedConfig.EditorScenarioActs.Remove(_selectedConfig.EditorScenarioActs[_selectedConfig.EditorScenarioActs.Count - 1]);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawScenarioActs()
        {
            foreach(var act in _selectedConfig.EditorScenarioActs)
            {
                DrawAct(act);
            }


            EditorUtility.SetDirty(_selectedConfig);
        }

        private void DrawAct(EditorScenarioActData act)
        {
            EditorGUILayout.BeginHorizontal("badge");

            act.Type = (ScenarioType)EditorGUILayout.EnumPopup("Act type", act.Type);

            switch (act.Type)
            {
                case ScenarioType.Unknown:
                    EditorGUILayout.LabelField("Choose scenario act type");
                    break;
                case ScenarioType.Horde:
                    DrawHordeScenarioAct(act);
                    break;
                case ScenarioType.CapturePoint:
                    DrawCaptuirePointScenarioAct(act);
                    break;
                case ScenarioType.Boss:
                    DrawBossScenarioAct(act);
                    break;
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawHordeScenarioAct(EditorScenarioActData act)
        {
            GUI.color = Color.cyan;
            EditorGUILayout.BeginVertical();
            foreach (var enemyData in act.EnemyData)
            {
                DrawHordeEnemyData(enemyData);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical();
            if (GUILayout.Button("+"))
            {
                act.EnemyData.Add(new HordeEnemyData());
            }
            if (GUILayout.Button("-"))
            {
                if(act.EnemyData.Count == 0)
                {
                    EditorGUILayout.EndVertical();
                    return;
                }
                act.EnemyData.Remove(act.EnemyData[act.EnemyData.Count - 1]);
            }
            EditorGUILayout.EndVertical();
            GUI.color = Color.white;
        }

        private void DrawHordeEnemyData(HordeEnemyData hordeEnemyData)
        {
            EditorGUILayout.BeginHorizontal("badge");

            hordeEnemyData.UnitType = (UnitType)EditorGUILayout.EnumPopup("Unit type", hordeEnemyData.UnitType);
            hordeEnemyData.CountOfEnemies = EditorGUILayout.IntField("Count of enemies", hordeEnemyData.CountOfEnemies);
            
            EditorGUILayout.EndHorizontal();
        }

        private void DrawCaptuirePointScenarioAct(EditorScenarioActData act)
        {
            GUI.color = Color.yellow;

            act.TimeToCapture = EditorGUILayout.FloatField("Time to capture", act.TimeToCapture);

            GUI.color = Color.white;
        }

        private void DrawBossScenarioAct(EditorScenarioActData act)
        {
            GUI.color = Color.red;

            act.BossPrefab = EditorGUILayout.ObjectField("Boss prefab", act.BossPrefab, typeof(GameObject), false) as GameObject;

            GUI.color = Color.white;
        }
    }
}