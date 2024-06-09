using ArenaShooter.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// Позволяет менять оружие, предоставляет доступ к выбранному оружию
    /// </summary>
    public sealed class WeaponSetController : MonoBehaviour
    {
        private BaseInputController _inputController;

        [SerializeField]
        private WeaponShootLHandler[] _weapons;
        [SerializeField]
        private int _selectedWeaponIndex;
        public WeaponShootLHandler CurrentWeapon => _weapons[_selectedWeaponIndex];

        public event Action WeaponChanged;

        [Inject]
        private void Construct(BaseInputController inputController)
        {
            _inputController = inputController;
        }

        private void OnEnable()
        {
            _inputController.ChangeWeaponUp += OnChangeWeaponUp;
            _inputController.ChangeWeaponDown += OnChangeWeaponDown;

        }

        private void OnDisable()
        {
            _inputController.ChangeWeaponUp -= OnChangeWeaponUp;
            _inputController.ChangeWeaponDown -= OnChangeWeaponDown;
        }

        private void OnChangeWeaponUp()
        {
            _selectedWeaponIndex++;
            if (_selectedWeaponIndex > _weapons.Length - 1)
            {
                _selectedWeaponIndex = 0;
            }
            ActivateSelectedWeapon();
            WeaponChanged?.Invoke();
        }

        private void OnChangeWeaponDown()
        {
            _selectedWeaponIndex--;
            if(_selectedWeaponIndex < 0)
            {
                _selectedWeaponIndex = _weapons.Length - 1;
            }
            ActivateSelectedWeapon();
            WeaponChanged?.Invoke();
        }

        private void ActivateSelectedWeapon()
        {
            for(int i = 0; i <  _weapons.Length; i++)
            {
                _weapons[i].gameObject.SetActive(false);
            }
            CurrentWeapon.gameObject.SetActive(true);
        }
        
    }
}