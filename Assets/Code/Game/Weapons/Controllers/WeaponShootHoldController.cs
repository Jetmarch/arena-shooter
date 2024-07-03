using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class WeaponShootHoldController : IInitializable, ILateDisposable
    {
        private BaseWeaponShootMechanic _weaponShootMechanic;
        private IShootInputProvider _shootInputProvider;

        public WeaponShootHoldController(BaseWeaponShootMechanic weaponShootMechanic, IShootInputProvider shootInputProvider)
        {
            _weaponShootMechanic = weaponShootMechanic;
            _shootInputProvider = shootInputProvider;
        }

        public void Initialize()
        {
            _shootInputProvider.OnShootHold += _weaponShootMechanic.Shoot;
        }

        public void LateDispose()
        {
            _shootInputProvider.OnShootHold -= _weaponShootMechanic.Shoot;
        }
    }
}