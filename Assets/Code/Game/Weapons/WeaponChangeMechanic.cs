using ArenaShooter.Inputs;
using ArenaShooter.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// Позволяет менять оружие, предоставляет доступ к выбранному оружию
    /// </summary>
    public sealed class WeaponChangeMechanic : MonoBehaviour
    {
        [SerializeField]
        private int _selectedWeaponIndex;
        
        private WeaponsStorage _weaponStorage;
        private IChangeWeaponInputProvider _inputController;

        private CompositeCondition _condition;

        public CompositeCondition Condition { get { return _condition; } }

        public event Action WeaponChanged;
        public GameObject CurrentWeapon => _weaponStorage.Weapons.ElementAt(_selectedWeaponIndex);

        public void Construct(IChangeWeaponInputProvider inputController, WeaponsStorage weaponStorage)
        {
            _inputController = inputController;
            _weaponStorage = weaponStorage;
            _condition = new CompositeCondition();
            _inputController.OnChangeWeaponUp += OnChangeWeaponUp;
            _inputController.OnChangeWeaponDown += OnChangeWeaponDown;
        }

        private void OnEnable()
        {
            if (_inputController == null) return;
            _inputController.OnChangeWeaponUp += OnChangeWeaponUp;
            _inputController.OnChangeWeaponDown += OnChangeWeaponDown;
        }

        private void OnDisable()
        {
            if (_inputController == null) return;
            _inputController.OnChangeWeaponUp -= OnChangeWeaponUp;
            _inputController.OnChangeWeaponDown -= OnChangeWeaponDown;
        }

        private void OnChangeWeaponUp()
        {
            if (!CanChangeWeapon()) return;
            _selectedWeaponIndex++;
            if (_selectedWeaponIndex > _weaponStorage.Weapons.Count - 1)
            {
                _selectedWeaponIndex = 0;
            }
            ActivateSelectedWeapon();
            WeaponChanged?.Invoke();
        }

        private void OnChangeWeaponDown()
        {
            if (!CanChangeWeapon()) return;
            _selectedWeaponIndex--;
            if(_selectedWeaponIndex < 0)
            {
                _selectedWeaponIndex = _weaponStorage.Weapons.Count - 1;
            }
            ActivateSelectedWeapon();
            WeaponChanged?.Invoke();
        }

        private bool CanChangeWeapon()
        {
            if (!_condition.IsTrue())
                return false;
            return true;
        }

        private void ActivateSelectedWeapon()
        {
            foreach(var weapon in _weaponStorage.Weapons)
            {
                weapon.gameObject.SetActive(false);
            }
            CurrentWeapon.SetActive(true);
        }
        
    }
}