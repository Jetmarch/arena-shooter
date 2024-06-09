
using System;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    public class WeaponHandler : MonoBehaviour
    {
        //TODO: Перенести в SO
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

        public void Shoot() 
        {
            Instantiate(_projectilePrefab, transform.position, transform.rotation);
        }

        public void Reload() 
        {

        }
    }
}
