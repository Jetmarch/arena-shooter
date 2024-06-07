using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using UnityEngine;

namespace ArenaShooter.Units
{
    /// <summary>
    /// ѕозвол€ет стрел€ть из выбранного оружи€
    /// </summary>
    public sealed class UnitShootController : MonoBehaviour
    {
        private BaseInputController _inputController;
        private WeaponSetController _weaponSet;

        private void Start()
        {
            _inputController = GetComponent<BaseInputController>();
            _weaponSet = GetComponent<WeaponSetController>();

            _inputController.Shoot += OnShoot;
        }

        private void OnEnable()
        {
            if (_inputController == null) return;
            _inputController.Shoot += OnShoot;
        }

        private void OnDisable()
        {
            _inputController.Shoot -= OnShoot;
        }

        public void OnShoot()
        {
            _weaponSet.CurrentWeapon.Shoot();
            Debug.Log("Shoot");
        }
    }
}