using ArenaShooter.Inputs;
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
    /// ѕозвол€ет мен€ть оружие, предоставл€ет доступ к выбранному оружию
    /// </summary>
    public sealed class WeaponChangeMechanic : MonoBehaviour
    {
        [SerializeField]
        private int _selectedWeaponIndex;
        
        private WeaponsStorage _weaponStorage;
        private IChangeWeaponInputProvider _inputController;

        public event Action WeaponChanged;
        public GameObject CurrentWeapon => _weaponStorage.Weapons.ElementAt(_selectedWeaponIndex);

        public void Construct(IChangeWeaponInputProvider inputController, WeaponsStorage weaponStorage)
        {
            _inputController = inputController;
            _weaponStorage = weaponStorage;
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
            //TODO: ѕрокидывать CompositeCondition извне
            if (CurrentWeapon.GetComponent<WeaponConditionContainer>().IsReloading)
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