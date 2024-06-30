using System;
using UnityEngine;

namespace ArenaShooter.Units.Player
{
    public class ScorePointsStorage
    {
        private int _scorePoints;

        public int ScorePoints { get { return _scorePoints; } }

        public event Action OnScorePointsChanged;

        public ScorePointsStorage()
        {
            _scorePoints = 0;
        }

        public void AddScorePoints(int scorePoints)
        {
            _scorePoints += scorePoints;
            OnScorePointsChanged?.Invoke();

            Debug.Log($"Current score is {_scorePoints}");
        }
    }
}