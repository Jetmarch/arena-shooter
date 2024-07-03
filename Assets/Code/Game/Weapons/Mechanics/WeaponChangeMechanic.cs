using ArenaShooter.Utils;
using System;
using System.Linq;
using UnityEngine;

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

        private CompositeCondition _condition;

        public CompositeCondition Condition { get { return _condition; } }

        public event Action WeaponChanged;
        public WeaponFacade CurrentWeapon => _weaponStorage.Weapons.ElementAtOrDefault(_selectedWeaponIndex);

        public void Construct(WeaponsStorage weaponStorage)
        {
            _weaponStorage = weaponStorage;
            _condition = new CompositeCondition();
        }

        public void OnChangeWeaponUp()
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

        public void OnChangeWeaponDown()
        {
            if (!CanChangeWeapon()) return;
            _selectedWeaponIndex--;
            if (_selectedWeaponIndex < 0)
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
            foreach (var weapon in _weaponStorage.Weapons)
            {
                weapon.gameObject.SetActive(false);
            }
            CurrentWeapon.gameObject.SetActive(true);
        }

    }
}