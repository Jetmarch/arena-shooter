using ArenaShooter.Inputs;
using ArenaShooter.Utils;
using ArenaShooter.Weapons.Projectiles;
using System;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    public abstract class BaseWeaponShootMechanic : MonoBehaviour
    {
        [SerializeField]
        protected Transform _projectileSpawnPoint;
        [SerializeField]
        protected ProjectileType _projectileType;
        protected ProjectileFactory _projectileFactory;

        private CompositeCondition _condition;
        public CompositeCondition Condition { get { return _condition; } }

        public event Action ShootComplete;

        public virtual void Construct(ProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;

            _condition = new CompositeCondition();
        }

        protected bool CanShoot()
        {
            if (!_condition.IsTrue()) return false;
            return true;
        }

        protected void OnShootComplete()
        {
            ShootComplete?.Invoke();
        }

        public virtual void OnShoot()
        {
            if (!CanShoot()) return;

            ShootMechanic();

            OnShootComplete();
        }

        public abstract void ShootMechanic();
    }
}
