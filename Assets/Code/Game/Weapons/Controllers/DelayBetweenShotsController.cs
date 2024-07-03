using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class DelayBetweenShotsController : IInitializable, ILateDisposable
    {
        private WeaponDelayBetweenShotsMechanic _delayBetweenShotsMechanic;
        private BaseWeaponShootMechanic _weaponShootMechanic;

        public DelayBetweenShotsController(WeaponDelayBetweenShotsMechanic delayBetweenShotsMechanic, BaseWeaponShootMechanic weaponShootMechanic)
        {
            _delayBetweenShotsMechanic = delayBetweenShotsMechanic;
            _weaponShootMechanic = weaponShootMechanic;
        }

        public void Initialize()
        {
            _weaponShootMechanic.ShootComplete += _delayBetweenShotsMechanic.DelayShot;
        }

        public void LateDispose()
        {
            _weaponShootMechanic.ShootComplete -= _delayBetweenShotsMechanic.DelayShot;
        }
    }
}