using UnityEngine;

namespace ArenaShooter.Weapons
{
    public class WeaponFacade : MonoBehaviour
    {
        [SerializeField]
        private AmmoClipStorage _ammoClipStorage;

        [SerializeField]
        private BaseWeaponShootMechanic _weaponShootMechanic;
        [SerializeField]
        private WeaponRotateMechanic _weaponRotateMechanic;

        public AmmoClipStorage AmmoClipStorage { get { return _ammoClipStorage; } }
        public BaseWeaponShootMechanic WeaponShootMechanic { get { return _weaponShootMechanic; } }
        public WeaponRotateMechanic WeaponRotateMechanic { get { return _weaponRotateMechanic; } }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _ammoClipStorage = GetComponent<AmmoClipStorage>();
            _weaponShootMechanic = GetComponent<BaseWeaponShootMechanic>();
        }
#endif
    }
}