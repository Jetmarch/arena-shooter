using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class AmmoView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _ammoPrefab;

        [SerializeField]
        private Transform _ammoContainer;

        [SerializeField]
        private int _maxAmmo = 50;

        public void UpdateAmmo(int amountOfAmmo)
        {
            ClearAmmo();
            int amount = Mathf.Clamp(amountOfAmmo, 0, _maxAmmo);
            for(int i = 0; i < amount; i++)
            {
                CreateAmmo();
            }
        }

        private void ClearAmmo()
        {
            foreach(Transform ammo in _ammoContainer.transform)
            {
                Destroy(ammo.gameObject);
            }
        }

        private void CreateAmmo()
        {
            Instantiate(_ammoPrefab, _ammoContainer.transform);
        }
    }
}