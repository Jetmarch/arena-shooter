using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class AmmoInClipDecreaseMechanic : MonoBehaviour
    {
        [SerializeField]
        private int _amountOfAmmoOnOneShot = 1;

        private IShootInputProvider _inputController;
        private AmmoClipStorage _ammoClipStorage;

        public bool IsEnoughAmmoToShoot()
        {
            return _ammoClipStorage.CurrentAmmo >= _amountOfAmmoOnOneShot;
        }

        public void Construct(IShootInputProvider inputController, AmmoClipStorage ammoClipStorage)
        {
            _ammoClipStorage = ammoClipStorage;
            _inputController = inputController;
            _inputController.OnShoot += OnShoot;
        }

        private void OnEnable()
        {
            if (_inputController == null) return;
            _inputController.OnShoot += OnShoot;
        }

        private void OnDisable()
        {
            if (_inputController == null) return;
            _inputController.OnShoot -= OnShoot;
        }

        private void OnShoot()
        {
            if (!IsEnoughAmmoToShoot()) return;
            int currentAmmo = _ammoClipStorage.CurrentAmmo - _amountOfAmmoOnOneShot;
            _ammoClipStorage.SetCurrentAmmo(currentAmmo);
        }
    }
}