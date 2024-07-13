using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class WeaponViewContainer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _weaponViewPrefab;

        [SerializeField]
        private List<WeaponView> _weaponViews = new List<WeaponView>();

        public WeaponView CreateWeaponView()
        {
            var weaponView = Instantiate(_weaponViewPrefab, transform).GetComponent<WeaponView>();
            if (weaponView == null)
            {
                throw new System.Exception($"{_weaponViewPrefab.name} prefab does not contain WeaponView component!");
            }
            _weaponViews.Add(weaponView);
            return weaponView;
        }
    }
}