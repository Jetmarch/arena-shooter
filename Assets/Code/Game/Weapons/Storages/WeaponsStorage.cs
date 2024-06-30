using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// ��������� ������ ��� ���������
    /// ��������� �������� ������ � ������ ������, �������� ����� ��� �������
    /// </summary>
    public class WeaponsStorage : MonoBehaviour
    {
        [SerializeField]
        private List<WeaponFacade> _weapons;

        public IReadOnlyCollection<WeaponFacade> Weapons { get { return _weapons; } }
        public event Action<WeaponFacade> OnWeaponsChanged;

        public void AddWeapon(WeaponFacade weapon)
        {
            _weapons.Add(weapon);
            OnWeaponsChanged?.Invoke(weapon);
        }

        public void RemoveWeapon(WeaponFacade weapon)
        {
            _weapons.Remove(weapon);
            OnWeaponsChanged?.Invoke(weapon);
        }
    }
}