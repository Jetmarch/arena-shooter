using ArenaShooter.Utils;
using ArenaShooter.Projectiles;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public abstract class BaseWeaponShootMechanic : MonoBehaviour
    {
        [SerializeField]
        protected Transform _projectileSpawnPoint;
        [SerializeField]
        protected ProjectileType _projectileType;
        protected ProjectileFactory _projectileFactory;

        protected GameObject _owner;

        private CompositeCondition _condition = new CompositeCondition();
        public CompositeCondition Condition { get { return _condition; } }

        public event Action ShootComplete;

        [Inject]
        public virtual void Construct(ProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;
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

        public virtual void Shoot()
        {
            if (!CanShoot()) return;

            ShootMechanic();

            OnShootComplete();
        }

        public void SetOwner(GameObject onwer)
        {
            _owner = onwer;
        }

        protected abstract void ShootMechanic();
    }
}
