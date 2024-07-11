using ArenaShooter.Artefacts;
using ArenaShooter.Weapons;
using UnityEngine;
using Zenject;

namespace ArenaShooter.UI
{
    public class WeaponPresenter : IWeaponPresenter, IInitializable
    {
        private WeaponConfig _weaponConfig;
        private WeaponView _view;
        private WeaponSet _weaponSet;

        public WeaponType WeaponType => _weaponConfig.WeaponType;
        public Sprite Sprite => _weaponConfig.Sprite;
        public string Name => _weaponConfig.Name;
        public string Description => _weaponConfig.Description;

        public WeaponPresenter(WeaponConfig weaponConfig, WeaponView weaponView, WeaponSet weaponSet)
        {
            _weaponConfig = weaponConfig;
            _view = weaponView;
            _weaponSet = weaponSet;
        }

        public void ChooseWeapon(WeaponType weaponType)
        {
            _weaponSet.SecondaryWeapon = weaponType;
            Debug.Log(_weaponSet.SecondaryWeapon);
        }

        public void Initialize()
        {
            _view.Setup(this);
        }
    }
}