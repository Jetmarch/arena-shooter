using ArenaShooter.Inputs;
using ArenaShooter.Weapons.Projectiles;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    [RequireComponent(typeof(AmmoClipStorage))]
    [RequireComponent(typeof(BaseWeaponShootMechanic))]
    [RequireComponent(typeof(WeaponFlipSpriteMechanic))]
    [RequireComponent(typeof(AmmoInClipDecreaseMechanic))]
    [RequireComponent(typeof(WeaponReloadMechanic))]
    [RequireComponent(typeof(WeaponRotateMechanic))]
    public class WeaponInstaller : MonoBehaviour
    {
        [SerializeField]
        private AmmoClipStorage _ammoClipStorage;
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

        public void Construct(IShootInputProvider shootInputProvider, IScreenMouseMoveInputProvider mouseMoveInputProvider,
            IWorldMouseMoveInputProvider worldMouseMoveProvider, IReloadInputProvider reloadInputProvider,
            ProjectileFactory projectileFactory)
        {
            _shootMechanic.Construct(shootInputProvider, projectileFactory);
            _flipSpriteMechanic.Construct(mouseMoveInputProvider);
            _ammoInClipDecreaseMechanic.Construct(shootInputProvider, _ammoClipStorage);
            _weaponReloadMechanic.Construct(reloadInputProvider, _ammoClipStorage);
            _weaponRotateMechanic.Construct(worldMouseMoveProvider);

            _shootMechanic.Condition.Append(_weaponReloadMechanic.IsNotReloading);
            _shootMechanic.Condition.Append(_ammoInClipDecreaseMechanic.IsEnoughAmmoToShoot);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _ammoClipStorage = GetComponent<AmmoClipStorage>();
            _shootMechanic = GetComponent<BaseWeaponShootMechanic>();
            _flipSpriteMechanic = GetComponent<WeaponFlipSpriteMechanic>();
            _ammoInClipDecreaseMechanic = GetComponent<AmmoInClipDecreaseMechanic>();
            _weaponReloadMechanic = GetComponent<WeaponReloadMechanic>();
            _weaponRotateMechanic = GetComponent<WeaponRotateMechanic>();
        }
#endif
    }
}