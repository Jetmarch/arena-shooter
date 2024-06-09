using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    /// <summary>
    /// ѕозвол€ет стрел€ть из выбранного оружи€
    /// </summary>
    public sealed class UnitShootController : MonoBehaviour
    {
        private BaseInputController _inputController;
        private WeaponSetController _weaponSet;

        [Inject]
        private void Constuct(BaseInputController inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _weaponSet = GetComponent<WeaponSetController>();
        }

        private void OnEnable()
        {
            _inputController.Shoot += OnShoot;
        }

        private void OnDisable()
        {
            _inputController.Shoot -= OnShoot;
        }

        public void OnShoot()
        {
            _weaponSet.CurrentWeapon.Shoot();
            Debug.Log("Shoot");
        }
    }
}