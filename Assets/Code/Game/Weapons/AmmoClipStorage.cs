using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    public class AmmoClipStorage : MonoBehaviour
    {
        [SerializeField]
        private int _maxAmmo;
        [SerializeField]
        private int _minAmmo = 0;
        [SerializeField]
        private int _currentAmmo;

        public event Action<float> CurrentAmmoChanged;
        public event Action<float> MaxAmmoChanged;

        public int CurrentAmmo { get { return _currentAmmo; } }
        public int MaxAmmo { get { return _maxAmmo; } }
        public int MinAmmo { get { return _minAmmo; } }

        private void Awake()
        {
            _currentAmmo = _maxAmmo;
        }

        public void SetCurrentAmmo(int ammo)
        {
            _currentAmmo = ammo;
            CurrentAmmoChanged?.Invoke(_currentAmmo);
        }

        public void SetMaxHealth(int ammo)
        {
            _maxAmmo = ammo;
            MaxAmmoChanged?.Invoke(_maxAmmo);
        }
    }

    //[Serializable]
    //public class Storage<T> where T : struct
    //{
    //    [SerializeField]
    //    private T _maxAmount;
    //    [SerializeField]
    //    private T _minAmount;
    //    [SerializeField]
    //    private T _currentAmount;
    //}
}