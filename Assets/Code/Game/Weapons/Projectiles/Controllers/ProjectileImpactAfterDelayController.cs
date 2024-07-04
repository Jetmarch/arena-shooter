using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileImpactAfterDelayController : IInitializable, ILateDisposable
    {
        private ProjectileImpactAfterDelayMechanic _impactMechanic;
        private IProjectileDamageMechanic _damageMechanic;

        public ProjectileImpactAfterDelayController(ProjectileImpactAfterDelayMechanic impactMechanic, IProjectileDamageMechanic damageMechanic)
        {
            _impactMechanic = impactMechanic;
            _damageMechanic = damageMechanic;
        }

        public void Initialize()
        {
            _impactMechanic.OnImpact += OnImpact;
        }

        public void LateDispose()
        {
            _impactMechanic.OnImpact -= OnImpact;
        }

        private void OnImpact()
        {
            _damageMechanic.OnHit(null);
        }
    }
}