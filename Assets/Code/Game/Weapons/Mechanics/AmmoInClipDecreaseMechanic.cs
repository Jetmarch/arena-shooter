using ArenaShooter.Inputs;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    public class AmmoInClipDecreaseMechanic : MonoBehaviour
    {
        [SerializeField]
        private int _amountOfAmmoOnOneShot = 1;

        private AmmoClipStorage _ammoClipStorage;

        public bool IsEnoughAmmoToShoot()
        {
            return _ammoClipStorage.CurrentAmmo >= _amountOfAmmoOnOneShot;
        }

        public void Construct(AmmoClipStorage ammoClipStorage)
        {
            _ammoClipStorage = ammoClipStorage;
        }

        public void OnShootComplete()
        {
            if (!IsEnoughAmmoToShoot()) return;
            int currentAmmo = _ammoClipStorage.CurrentAmmo - _amountOfAmmoOnOneShot;
            _ammoClipStorage.SetCurrentAmmo(currentAmmo);
        }
    }
}