namespace ArenaShooter.Weapons
{
    public class AmmoInClipDecreaseMechanic
    {
        private int _amountOfAmmoOnOneShot = 1;

        private AmmoClipStorage _ammoClipStorage;

        public AmmoInClipDecreaseMechanic(int amountOfAmmoOnOneShot, AmmoClipStorage ammoClipStorage)
        {
            _amountOfAmmoOnOneShot = amountOfAmmoOnOneShot;
            _ammoClipStorage = ammoClipStorage;
        }

        public bool IsEnoughAmmoToShoot()
        {
            return _ammoClipStorage.CurrentAmmo >= _amountOfAmmoOnOneShot;
        }

        public void DecreaseAmmo()
        {
            if (!IsEnoughAmmoToShoot()) return;
            int currentAmmo = _ammoClipStorage.CurrentAmmo - _amountOfAmmoOnOneShot;
            _ammoClipStorage.SetCurrentAmmo(currentAmmo);
        }
    }
}