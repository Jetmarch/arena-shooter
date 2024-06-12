using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public sealed class WeaponReloadObserver : MonoBehaviour
    {
        private WeaponConditionContainer _weaponContainer;
        private BaseInputController _inputController;
        [Inject]
        private void Construct(BaseInputController inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _weaponContainer = GetComponent<WeaponConditionContainer>();
        }

        private void OnEnable()
        {
            _inputController.OnReload += OnReload;
        }

        private void OnDisable()
        {
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