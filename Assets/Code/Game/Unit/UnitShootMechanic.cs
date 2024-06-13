using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    /// <summary>
    /// ѕозвол€ет стрел€ть из выбранного оружи€
    /// </summary>
    [Obsolete]
    public sealed class UnitShootMechanic : MonoBehaviour
    {
        private IShootInputProvider _inputController;
        private WeaponSetController _weaponSet;

        [Inject]
        private void Constuct(IShootInputProvider inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _weaponSet = GetComponent<WeaponSetController>();
        }

        private void OnEnable()
        {
            _inputController.OnShoot += OnShoot;
        }

        private void OnDisable()
        {
            _inputController.OnShoot -= OnShoot;
        }

        public void OnShoot()
        {
            //_weaponSet.CurrentWeapon.OnShoot();
        }
    }
}