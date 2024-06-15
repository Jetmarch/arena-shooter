using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public sealed class WeaponReloadMechanic : MonoBehaviour
    {
        private WeaponConditionContainer _weaponContainer;
        private IReloadInputProvider _inputController;

        private bool _isReloading;

        public bool IsNotReloading()
        {
            return !_isReloading;
        }

        public void Construct(IReloadInputProvider inputController)
        {
            _inputController = inputController;
            _inputController.OnReload += OnReload;
        }

        private void Start()
        {
            _weaponContainer = GetComponent<WeaponConditionContainer>();
        }

        private void OnEnable()
        {
            if (_inputController == null) return;
            _inputController.OnReload += OnReload;
        }

        private void OnDisable()
        {
            if (_inputController == null) return;
            _inputController.OnReload -= OnReload;
        }

        public void OnReload()
        {
            if (_weaponContainer.IsReloading) return;

            StartCoroutine(Reloading());
        }

        private IEnumerator Reloading()
        {
            _weaponContainer.IsReloading = true;
            yield return new WaitForSeconds(_weaponContainer.ReloadSpeed);
            //TODO: Уменьшение боезапаса
            _weaponContainer.CurrentAmmoInClip = _weaponContainer.MaxAmmoInClip;
            _weaponContainer.IsReloading = false;
        }
    }
}