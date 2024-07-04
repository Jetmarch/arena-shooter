using ArenaShooter.Units;
using UnityEngine;
using Zenject;

namespace ArenaShooter.CameraScripts
{
    public class CameraShakeOnEnemyDieController : IInitializable, ILateDisposable
    {
        private UnitManager _unitManager;
        private CameraShakeMechanic _shakeMechanic;
        private CameraShakeData _shakeData;

        public CameraShakeOnEnemyDieController(UnitManager unitManager, CameraShakeMechanic shakeMechanic, CameraShakeData shakeData)
        {
            _unitManager = unitManager;
            _shakeMechanic = shakeMechanic;
            _shakeData = shakeData;
        }

        public void Initialize()
        {
            _unitManager.UnitDie += OnUnitDied;
        }

        public void LateDispose()
        {
            _unitManager.UnitDie -= OnUnitDied;
        }

        private void OnUnitDied(GameObject obj)
        {
            _shakeMechanic.ShakeCamera(_shakeData);
        }
    }
}