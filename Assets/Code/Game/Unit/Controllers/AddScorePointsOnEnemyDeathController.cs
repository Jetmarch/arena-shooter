using ArenaShooter.Units.Player;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    public class AddScorePointsOnEnemyDeathController : IInitializable, ILateDisposable
    {
        private UnitManager _unitManager;
        private ScorePointsStorage _scorePointsStorage;

        public AddScorePointsOnEnemyDeathController(UnitManager unitManager, ScorePointsStorage scorePointsStorage)
        {
            _unitManager = unitManager;
            _scorePointsStorage = scorePointsStorage;
        }

        public void Initialize()
        {
            _unitManager.UnitDie += AddScorePoints;
        }

        public void LateDispose()
        {
            _unitManager.UnitDie -= AddScorePoints;
        }

        private void AddScorePoints(GameObject deadUnit)
        {
            //TODO: �������� � ���������� ����������� �������� ���������� ����� �� ��� ��������
            var giveScorePointsMechanic = deadUnit.GetComponent<UnitGiveScorePointsMechanic>();
            if (giveScorePointsMechanic == null) return;
            _scorePointsStorage.AddScorePoints(giveScorePointsMechanic.GetScorePoints());
        }
    }
}