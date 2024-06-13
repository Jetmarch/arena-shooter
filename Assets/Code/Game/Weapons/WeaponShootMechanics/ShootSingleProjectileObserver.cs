using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class ShootSingleProjectileObserver : BaseWeaponShootMechanic
    {
        protected override void Start()
        {
            base.Start();
        }

        public override void OnShoot()
        {
            if (!CanShoot()) return;

            Instantiate(_weaponContainer.ProjectilePrefab, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
        }
    }
}