using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    public class ShootProjectileBurstObserver : BaseWeaponShootObserver
    {
        [SerializeField]
        private int _countOfProjectilesInShot = 4;

        [SerializeField]
        private float _minSpreadAngle = -5f;
        [SerializeField]
        private float _maxSpreadAngle = 5f;

        public override void OnShoot()
        {
            if (!CanShoot()) return;

            for (int i = 0; i < _countOfProjectilesInShot; i++)
            {
                var spreadRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + Random.Range(_minSpreadAngle, _maxSpreadAngle));
                Instantiate(_weaponContainer.ProjectilePrefab, transform.position, spreadRotation);
            }
        }
    }
}