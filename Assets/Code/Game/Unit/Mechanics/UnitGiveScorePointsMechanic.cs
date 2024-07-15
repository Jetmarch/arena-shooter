using UnityEngine;

namespace ArenaShooter.Units
{
    public class UnitGiveScorePointsMechanic : MonoBehaviour
    {
        [SerializeField]
        private int _amountOfPoints;

        public int GetScorePoints()
        {
            return _amountOfPoints;
        }
    }
}