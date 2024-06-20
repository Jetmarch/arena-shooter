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
        protected IShootInputProvider _inputController;
        protected ProjectileFactory _projectileFactory;

        private CompositeCondition _condition;
        public CompositeCondition Condition { get { return _condition; } }

        public event Action ShootComplete;

        public virtual void Construct(IShootInputProvider inputController, ProjectileFactory projectileFactory)
        {
            _inputController = inputController;
            _projectileFactory = projectileFactory;

            _condition = new CompositeCondition();
            _inputController.OnShoot += OnShoot;
        }

        protected virtual void OnEnable()
        {
            if (_inputController == null) return;
            _inputController.OnShoot += OnShoot;
        }

        protected virtual void OnDisable()
        {
            if (_inputController == null) return;
            _inputController.OnShoot -= OnShoot;
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

        public abstract void OnShoot();
    }
}
