using ArenaShooter.Inputs;
using System;
using System.Collections;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    //TODO: Выделить обойму, как отдельную сущность
    public sealed class WeaponReloadMechanic : MonoBehaviour
    {
        [SerializeField]
        private float _reloadSpeed;

        private AmmoClipStorage _ammoClipStorage;

        private bool _isReloading;

        public event Action OnStartReload;
        public event Action OnEndReload;

        public bool IsReloading { get { return _isReloading; } }

        public bool IsNotReloading()
        {
            return !_isReloading;
        }

        public void Construct(AmmoClipStorage ammoClipStorage)
        {
            _ammoClipStorage = ammoClipStorage;
        }

        public void OnReload()
        {
            if (_isReloading) return;

            StartCoroutine(Reloading());
        }

        private IEnumerator Reloading()
        {
            _isReloading = true;
            OnStartReload?.Invoke();

            yield return new WaitForSeconds(_reloadSpeed);

            _ammoClipStorage.SetCurrentAmmo(_ammoClipStorage.MaxAmmo);

            OnEndReload?.Invoke();
            _isReloading = false;
        }
    }
}