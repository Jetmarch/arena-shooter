using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// Поворачивает оружие персонажа в сторону указателя мыши
    /// </summary>
    public sealed class WeaponRotateMechanic : IInitializable, ILateDisposable, IGamePauseListener
    {
        private WeaponChangeMechanic _weaponChangeMechanic;
        private bool _isPaused;

        public WeaponRotateMechanic(WeaponChangeMechanic weaponChangeMechanic)
        {
            _weaponChangeMechanic = weaponChangeMechanic;
        }

        public void Initialize()
        {
            IGameLoopListener.Register(this);
        }

        public void LateDispose()
        {
            IGameLoopListener.Unregister(this);
        }

        public void OnPauseGame()
        {
            _isPaused = true;
        }

        public void OnResumeGame()
        {
            _isPaused = false;
        }

        public void RotateWeapon(Vector3 mousePos)
        {
            if (_isPaused) return;

            var currentWeapon = _weaponChangeMechanic.CurrentWeapon;
            if (currentWeapon == null) return;

            float angleRad = Mathf.Atan2(mousePos.y - currentWeapon.transform.position.y, mousePos.x - currentWeapon.transform.position.x);
            float angle = (180 / Mathf.PI) * angleRad;

            currentWeapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}