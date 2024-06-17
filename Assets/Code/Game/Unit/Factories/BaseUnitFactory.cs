using UnityEngine;

namespace ArenaShooter.Units.Factories
{
    public abstract class BaseUnitFactory : MonoBehaviour
    {
        [SerializeField]
        protected UnitType _unitType;
        [SerializeField]
        protected GameObject _unitPrefab;

        public UnitType Type { get { return _unitType; } }
        public abstract GameObject CreateUnit(Vector3 position, Transform parent);
    }

    public enum UnitType
    {
        Unknown,
        Player,
        EnemyShooter,
        EnemyMelee,
        EnemyKamikaze,
        Boss
    }
}