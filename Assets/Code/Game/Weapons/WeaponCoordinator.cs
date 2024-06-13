using ArenaShooter.Inputs;
using ArenaShooter.Weapons.Projectiles;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class WeaponCoordinator : MonoBehaviour
    {
        [SerializeField]
        private BaseWeaponShootMechanic _shootMechanic;

        [SerializeField]
        private WeaponFlipSpriteMechanic _flipSpriteMechanic;

        [SerializeField]
        private AmmoInClipDecreaseMechanic _ammoInClipDecreaseMechanic;

        [SerializeField]
        private WeaponReloadMechanic _weaponReloadMechanic;

        [SerializeField]
        private WeaponRotateMechanic _weaponRotateMechanic;

        public void Construct(IShootInputProvider shootInputProvider, IMouseMoveInputProvider mouseMoveInputProvider, IReloadInputProvider reloadInputProvider, ProjectileFactory projectileFactory)
        {
            _shootMechanic.Construct(shootInputProvider, projectileFactory);
            _flipSpriteMechanic.Construct(mouseMoveInputProvider);
            _ammoInClipDecreaseMechanic.Construct(shootInputProvider);
            _weaponReloadMechanic.Construct(reloadInputProvider);
            _weaponRotateMechanic.Construct(mouseMoveInputProvider);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _shootMechanic = GetComponent<BaseWeaponShootMechanic>();
            _flipSpriteMechanic = GetComponent<WeaponFlipSpriteMechanic>();
            _ammoInClipDecreaseMechanic = GetComponent<AmmoInClipDecreaseMechanic>();
            _weaponReloadMechanic = GetComponent<WeaponReloadMechanic>();
            _weaponRotateMechanic = GetComponent<WeaponRotateMechanic>();
        }
#endif
    }
}