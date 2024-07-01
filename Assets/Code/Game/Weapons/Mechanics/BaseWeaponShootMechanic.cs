using ArenaShooter.Utils;
using ArenaShooter.Weapons.Projectiles;
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

        private CompositeCondition _condition;
        public CompositeCondition Condition { get { return _condition; } }

        public event Action ShootComplete;

        [Inject]
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

        public void SetOwner(GameObject onwer)
        {
            _owner = onwer;
        }

        protected abstract void ShootMechanic();
    }
}
