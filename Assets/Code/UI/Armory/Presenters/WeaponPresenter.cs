using ArenaShooter.Weapons;
using System;
using TMPro;
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
        private TextMeshProUGUI _descriptionLabel;

        public WeaponType WeaponType => _weaponConfig.WeaponType;
        public Sprite Sprite => _weaponConfig.Sprite;
        public string Name => _weaponConfig.Name;
        public string Description => _weaponConfig.Description;

        public Action<bool> OnWeaponChoosed;

        public WeaponPresenter(WeaponConfig weaponConfig, WeaponView weaponView, WeaponSet weaponSet, IItemContainerPresenter weaponContainerPresenter, TextMeshProUGUI descriptionLabel)
        {
            _weaponConfig = weaponConfig;
            _view = weaponView;
            _weaponSet = weaponSet;
            _weaponContainerPresenter = weaponContainerPresenter;
            _descriptionLabel = descriptionLabel;
        }

        public void SelectWeapon(WeaponType weaponType)
        {
            _weaponSet.SecondaryWeapon = weaponType;
            _weaponContainerPresenter.ClearSelectedItem();
            _view.SelectItemAnimation();
        }

        public void Initialize()
        {
            SetSelectState();
            _view.Setup(this);
        }

        private void SetSelectState()
        {
            if(_weaponSet.SecondaryWeapon == _weaponConfig.WeaponType)
            {
                SelectWeapon(_weaponConfig.WeaponType);
            }
        }

        public void ShowDescription()
        {
            _descriptionLabel.text = Description;
        }

        public void HideDescription()
        {
            _descriptionLabel.text = string.Empty;
        }
    }
}