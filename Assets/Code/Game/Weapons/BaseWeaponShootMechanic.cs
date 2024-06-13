using ArenaShooter.Inputs;
using ArenaShooter.Utils;
using ArenaShooter.Weapons.Projectiles;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    public abstract class BaseWeaponShootMechanic : MonoBehaviour
    {
        [SerializeField]
        protected Transform _projectileSpawnPoint;
        protected WeaponConditionContainer _weaponContainer;
        protected IShootInputProvider _inputController;
        protected ProjectileFactory _projectileFactory;

        private CompositeCondition _condition;
        public CompositeCondition Condition { get { return _condition; } }

        public void Construct(IShootInputProvider inputController, ProjectileFactory projectileFactory)
        {
            _inputController = inputController;
            _projectileFactory = projectileFactory;

            //_inputController.OnShoot += OnShoot;
        }

        protected virtual void Start()
        {
            _weaponContainer = GetComponent<WeaponConditionContainer>();
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
            //TODO: Заменить на CompositeCondition
            if (_weaponContainer.IsReloading) return false;
            if (_weaponContainer.CurrentAmmoInClip <= 0f) return false;
            return true;
        }

        public abstract void OnShoot();
    }
}
