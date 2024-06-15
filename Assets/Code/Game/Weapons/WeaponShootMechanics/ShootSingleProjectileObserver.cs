using ArenaShooter.Inputs;
using ArenaShooter.Weapons.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class ShootSingleProjectileObserver : BaseWeaponShootMechanic
    {
        public override void OnShoot()
        {
            if (!CanShoot()) return;

            //TODO: Передавать пул снарядов в качестве родительского объекта
            _projectileFactory.CreateProjectile(_projectileType, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
        }
    }
}