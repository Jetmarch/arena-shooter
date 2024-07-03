using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class WeaponReloadController : IInitializable, ILateDisposable
    {
        private WeaponReloadMechanic _weaponReloadMechanic;
        private IReloadInputProvider _reloadInputProvider;

        public WeaponReloadController(WeaponReloadMechanic weaponReloadMechanic, IReloadInputProvider reloadInputProvider)
        {
            _weaponReloadMechanic = weaponReloadMechanic;
            _reloadInputProvider = reloadInputProvider;
        }

        public void Initialize()
        {
            _reloadInputProvider.OnReload += _weaponReloadMechanic.OnReload;
        }

        public void LateDispose()
        {
            _reloadInputProvider.OnReload -= _weaponReloadMechanic.OnReload;
        }
    }
}