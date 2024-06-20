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
    [RequireComponent(typeof(WeaponDelayBetweenShotsMechanic))]
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
        [SerializeField]
        private WeaponDelayBetweenShotsMechanic _delayBetweenShotsMechanic;

        //TODO: Продумать автоматическую подвязку оружия к носителю
        public void Construct(IShootInputProvider shootInputProvider, IScreenMouseMoveInputProvider mouseMoveInputProvider,
            IWorldMouseMoveInputProvider worldMouseMoveProvider, IReloadInputProvider reloadInputProvider,
            ProjectileFactory projectileFactory)
        {
            _flipSpriteMechanic.Construct(mouseMoveInputProvider);
            
            _weaponReloadMechanic.Construct(reloadInputProvider, _ammoClipStorage);
            _weaponRotateMechanic.Construct(worldMouseMoveProvider);

            _delayBetweenShotsMechanic.Construct(shootInputProvider);

            _shootMechanic.Construct(shootInputProvider, projectileFactory);
            _shootMechanic.Condition.Append(_weaponReloadMechanic.IsNotReloading);
            _shootMechanic.Condition.Append(_ammoInClipDecreaseMechanic.IsEnoughAmmoToShoot);
            _shootMechanic.Condition.Append(_delayBetweenShotsMechanic.CanShoot);

            _ammoInClipDecreaseMechanic.Construct(_ammoClipStorage);
            _shootMechanic.ShootComplete += _ammoInClipDecreaseMechanic.OnShootComplete;
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
            _delayBetweenShotsMechanic = GetComponent<WeaponDelayBetweenShotsMechanic>();
        }
#endif
    }
}