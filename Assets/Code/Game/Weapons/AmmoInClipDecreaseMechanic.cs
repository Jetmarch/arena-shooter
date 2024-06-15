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

        [SerializeField]
        private int _currentAmmoInClip;

        private IShootInputProvider _inputController;

        public bool IsEnoughAmmoToShoot()
        {
            return _amountOfAmmoOnOneShot > _currentAmmoInClip;
        }

        public void Construct(IShootInputProvider inputController)
        {
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

            _currentAmmoInClip -= _amountOfAmmoOnOneShot;
        }
    }
}