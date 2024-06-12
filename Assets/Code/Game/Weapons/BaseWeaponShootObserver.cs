
using ArenaShooter.Inputs;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public abstract class BaseWeaponShootObserver : MonoBehaviour
    {
        protected WeaponConditionContainer _weaponContainer;
        protected BaseInputController _inputController;

        [Inject]
        private void Construct(BaseInputController inputController)
        {
            _inputController = inputController;
        }

        protected virtual void Start()
        {
            _weaponContainer = GetComponent<WeaponConditionContainer>();
        }

        protected virtual void OnEnable()
        {
            _inputController.OnShoot += OnShoot;
        }

        protected virtual void OnDisable()
        {
            _inputController.OnShoot -= OnShoot;
        }

        protected bool CanShoot()
        {
            if (_weaponContainer.IsReloading) return false;
            if (_weaponContainer.CurrentAmmoInClip <= 0f) return false;
            return true;
        }

        public abstract void OnShoot();
    }
}
