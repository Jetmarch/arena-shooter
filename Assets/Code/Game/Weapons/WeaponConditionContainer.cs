using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    [Obsolete]
    public sealed class WeaponConditionContainer : MonoBehaviour
    {
        [SerializeField]
        private int maxAmmoInBackpack;
        [SerializeField]
        private int currentAmmoInBackpack;
        [SerializeField]
        private int maxAmmoInClip;
        [SerializeField]
        private int currentAmmoInClip;
        [SerializeField]
        private float reloadSpeed;
        [SerializeField]
        private float rateOfFire;
        [SerializeField]
        private GameObject _projectilePrefab;
        [SerializeField]
        private bool _isReloading;

        public int MaxAmmoInBackpack { get => maxAmmoInBackpack; set => maxAmmoInBackpack = value; }
        public int CurrentAmmoInBackpack { get => currentAmmoInBackpack; set => currentAmmoInBackpack = value; }
        public int MaxAmmoInClip { get => maxAmmoInClip; set => maxAmmoInClip = value; }
        public int CurrentAmmoInClip { get => currentAmmoInClip; set => currentAmmoInClip = value; }
        public float ReloadSpeed { get => reloadSpeed; set => reloadSpeed = value; }
        public float RateOfFire { get => rateOfFire; set => rateOfFire = value; }
        //TODO: Заменить на ProjectileType
        public GameObject ProjectilePrefab { get => _projectilePrefab; set => _projectilePrefab = value; }
        public bool IsReloading { get => _isReloading; set => _isReloading = value; }
    }
}