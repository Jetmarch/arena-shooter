using ArenaShooter.Units.Player;
using ArenaShooter.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.UI
{
    public class AmmoController : IInitializable, ILateDisposable
    {
        private AmmoClipStorage _ammoClipStorage;
        private AmmoView _ammoView;

        public AmmoController(PlayerFacade player, AmmoView ammoView)
        {
            _ammoView = ammoView;
        }

        public void Initialize()
        {
            _ammoClipStorage.CurrentAmmoChanged += _ammoView.UpdateAmmo;
        }

        public void LateDispose()
        {
            _ammoClipStorage.CurrentAmmoChanged -= _ammoView.UpdateAmmo;
        }
    }
}