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

        private WeaponConditionContainer _weaponConditionContainer;
        private IShootInputProvider _inputController;

        public void Construct(IShootInputProvider inputController)
        {
            _inputController = inputController;
            _inputController.OnShoot += OnShoot;
        }

        private void Start()
        {
            _weaponConditionContainer = GetComponent<WeaponConditionContainer>();
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
            if (_weaponConditionContainer.CurrentAmmoInClip < _amountOfAmmoOnOneShot) return;

            _weaponConditionContainer.CurrentAmmoInClip -= _amountOfAmmoOnOneShot;
        }
    }
}