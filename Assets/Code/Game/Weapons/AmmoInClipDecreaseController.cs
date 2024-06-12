using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{

    public class AmmoInClipDecreaseController : MonoBehaviour
    {
        [SerializeField]
        private int _amountOfAmmoOnOneShot = 1;

        private WeaponConditionContainer _weaponConditionContainer;
        private BaseInputController _inputController;

        [Inject]
        private void Construct(BaseInputController inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _weaponConditionContainer = GetComponent<WeaponConditionContainer>();
        }

        private void OnEnable()
        {
            _inputController.OnShoot += OnShoot;
        }

        private void OnDisable()
        {
            _inputController.OnShoot -= OnShoot;
        }

        private void OnShoot()
        {
            if (_weaponConditionContainer.CurrentAmmoInClip < _amountOfAmmoOnOneShot) return;

            _weaponConditionContainer.CurrentAmmoInClip -= _amountOfAmmoOnOneShot;
        }
    }
}