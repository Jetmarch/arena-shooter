using ArenaShooter.Components;
using ArenaShooter.Weapons;
using UnityEngine;

namespace ArenaShooter.Units.Player
{
    public class PlayerFacade : MonoBehaviour
    {
        [SerializeField]
        private WeaponsStorage _weaponsStorage;

        [SerializeField]
        private WeaponChangeMechanic _weaponChangeMechanic;

        [SerializeField]
        private HealthComponent _healthComponent;

        public WeaponsStorage WeaponsStorage { get { return _weaponsStorage; } }
        public HealthComponent HealthComponent { get { return _healthComponent; } }
        public WeaponChangeMechanic WeaponChangeMechanic { get {  return _weaponChangeMechanic; } }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _weaponsStorage = GetComponent<WeaponsStorage>();
            _healthComponent = GetComponent<HealthComponent>();
            _weaponChangeMechanic = GetComponent<WeaponChangeMechanic>();
        }
#endif
    }
}