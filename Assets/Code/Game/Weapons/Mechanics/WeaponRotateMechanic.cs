using UnityEngine;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// Поворачивает оружие персонажа в сторону указателя мыши
    /// </summary>
    public sealed class WeaponRotateMechanic
    {
        private WeaponChangeMechanic _weaponChangeMechanic;

        public WeaponRotateMechanic(WeaponChangeMechanic weaponChangeMechanic)
        {
            _weaponChangeMechanic = weaponChangeMechanic;
        }

        public void RotateWeapon(Vector3 mousePos)
        {
            var currentWeapon = _weaponChangeMechanic.CurrentWeapon;
            if (currentWeapon == null) return;

            float angleRad = Mathf.Atan2(mousePos.y - currentWeapon.transform.position.y, mousePos.x - currentWeapon.transform.position.x);
            float angle = (180 / Mathf.PI) * angleRad;

            currentWeapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}