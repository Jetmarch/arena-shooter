using ArenaShooter.Projectiles;
using ArenaShooter.Utils;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public abstract class BaseWeaponShootMechanic : MonoBehaviour, IGamePauseListener
    {
        [SerializeField]
        protected Transform _projectileSpawnPoint;
        [SerializeField]
        protected ProjectileType _projectileType;
        protected ProjectileFactory _projectileFactory;

        protected GameObject _owner;

        private CompositeCondition _condition = new CompositeCondition();

        private bool _isPaused;

        public CompositeCondition Condition { get { return _condition; } }

        public event Action ShootComplete;

        [Inject]
        public virtual void Construct(ProjectileFactory projectileFactory)
        {
            _projectileFactory = projectileFactory;
        }

        protected bool CanShoot()
        {
            if (_isPaused) return false;
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

        public abstract void ShootMechanic();

        public void OnPauseGame()
        {
            _isPaused = true;
        }

        public void OnResumeGame()
        {
            _isPaused = false;
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }
    }
}
