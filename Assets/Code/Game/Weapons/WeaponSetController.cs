using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// ��������� ������ ������, ������������� ������ � ���������� ������
    /// </summary>
    public sealed class WeaponSetController : MonoBehaviour
    {
        private BaseInputController _inputController;

        [SerializeField]
        private Weapon[] _weapons;

        private int _selectedWeaponIndex;
        public Weapon CurrentWeapon => _weapons[_selectedWeaponIndex];

    }
}