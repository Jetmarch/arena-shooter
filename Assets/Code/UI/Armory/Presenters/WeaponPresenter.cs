using ArenaShooter.Weapons;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.UI
{
    public class WeaponPresenter : IWeaponPresenter, IInitializable
    {
        private WeaponConfig _weaponConfig;
        private WeaponView _view;
        private WeaponSet _weaponSet;
        private IItemContainerPresenter _weaponContainerPresenter;

        private bool _isChoosed;

        public WeaponType WeaponType => _weaponConfig.WeaponType;
        public Sprite Sprite => _weaponConfig.Sprite;
        public string Name => _weaponConfig.Name;
        public string Description => _weaponConfig.Description;
        public bool IsChoosed => _isChoosed;

        public Action<bool> OnWeaponChoosed;

        public WeaponPresenter(WeaponConfig weaponConfig, WeaponView weaponView, WeaponSet weaponSet, IItemContainerPresenter weaponContainerPresenter)
        {
            _weaponConfig = weaponConfig;
            _view = weaponView;
            _weaponSet = weaponSet;
            _weaponContainerPresenter = weaponContainerPresenter;
        }

        public void SelectWeapon(WeaponType weaponType)
        {
            _weaponSet.SecondaryWeapon = weaponType;
            _weaponContainerPresenter.ClearSelectedItem();
        }

        public void Initialize()
        {
            SetChooseState();
            _view.Setup(this);
        }

        private void SetChooseState()
        {
            if(_weaponSet.SecondaryWeapon == _weaponConfig.WeaponType)
            {
                _isChoosed = true;
            }
            else
            {
                _isChoosed= false;
            }
        }
    }
}