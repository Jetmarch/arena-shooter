using UnityEngine;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// Поворачивает оружие персонажа в сторону указателя мыши
    /// </summary>
    public sealed class WeaponRotateMechanic : MonoBehaviour
    {
        public void RotateWeapon(Vector3 mousePos)
        {
            float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
            float angle = (180 / Mathf.PI) * angleRad;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}