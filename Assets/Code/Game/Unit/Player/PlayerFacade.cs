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
        private HealthComponent _healthComponent;

        public WeaponsStorage WeaponsStoraget { get { return _weaponsStorage; } }
        public HealthComponent HealthComponent { get { return _healthComponent; } }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _weaponsStorage = GetComponent<WeaponsStorage>();
            _healthComponent = GetComponent<HealthComponent>();
        }
#endif
    }
}