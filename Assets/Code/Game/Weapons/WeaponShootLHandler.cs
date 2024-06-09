
using System;
using System.Collections;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    public class WeaponShootLHandler : MonoBehaviour
    {
        private WeaponConditionContainer _weaponContainer;

        private void Start()
        {
            _weaponContainer = GetComponent<WeaponConditionContainer>();
        }

        public void Shoot() 
        {
            if (_weaponContainer.IsReloading) return;
            if (_weaponContainer.CurrentAmmoInClip <= 0f) return;


            Instantiate(_weaponContainer.ProjectilePrefab, transform.position, transform.rotation);
            _weaponContainer.CurrentAmmoInClip--;
        }
    }
}
