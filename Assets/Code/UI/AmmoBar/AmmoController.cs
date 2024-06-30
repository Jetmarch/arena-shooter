using ArenaShooter.Units.Player;
using ArenaShooter.Weapons;
using Zenject;

namespace ArenaShooter.UI
{
    public class AmmoController : IInitializable, ILateDisposable
    {
        private AmmoClipStorage _ammoClipStorage;
        private AmmoView _ammoView;
        private WeaponChangeMechanic _weaponChangeMechanic;

        public AmmoController(IPlayerProvider provider, AmmoView ammoView)
        {
            _weaponChangeMechanic = provider.Player.WeaponChangeMechanic;
            _ammoView = ammoView;
        }

        public void Initialize()
        {
            _weaponChangeMechanic.WeaponChanged += OnWeaponChanged;
        }
        public void LateDispose()
        {
            _weaponChangeMechanic.WeaponChanged -= OnWeaponChanged;

            if (_ammoClipStorage == null) return; 
            _ammoClipStorage.CurrentAmmoChanged -= _ammoView.UpdateAmmo;
        }

        private void OnWeaponChanged()
        {
            if(_ammoClipStorage != null)
            {
                _ammoClipStorage.CurrentAmmoChanged -= _ammoView.UpdateAmmo;
            }
            _ammoClipStorage = _weaponChangeMechanic.CurrentWeapon.AmmoClipStorage;
            _ammoClipStorage.CurrentAmmoChanged += _ammoView.UpdateAmmo;
            _ammoView.UpdateAmmo(_ammoClipStorage.CurrentAmmo);
        }
    }
}