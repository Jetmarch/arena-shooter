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
    public class BaseWeaponInstaller : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _weaponSprite;
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

        [SerializeField]
        //Сделал для удобства одно условие здесь,
        //так как это единственная разница между автоматами и остальным оружием
        //на текущий момент
        private bool _isAutomatic;

        private IShootInputProvider _shootInputProvider;
        private IScreenMouseMoveInputProvider _screenMouseMoveInputProvider;
        private IWorldMouseMoveInputProvider _worldMouseMoveInputProvider;
        private IReloadInputProvider _reloadInputProvider;

        //TODO: Продумать автоматическую подвязку оружия к носителю
        public void Construct(IShootInputProvider shootInputProvider, IScreenMouseMoveInputProvider mouseMoveInputProvider,
            IWorldMouseMoveInputProvider worldMouseMoveProvider, IReloadInputProvider reloadInputProvider,
            ProjectileFactory projectileFactory)
        {
            _shootInputProvider = shootInputProvider;
            _screenMouseMoveInputProvider = mouseMoveInputProvider;
            _worldMouseMoveInputProvider = worldMouseMoveProvider;
            _reloadInputProvider = reloadInputProvider;

            _flipSpriteMechanic.Construct(_weaponSprite);
            _screenMouseMoveInputProvider.OnScreenMouseMove += _flipSpriteMechanic.FlipWeaponSprite;
            
            _weaponReloadMechanic.Construct(_ammoClipStorage);
            _reloadInputProvider.OnReload += _weaponReloadMechanic.OnReload;

            _worldMouseMoveInputProvider.OnWorldMouseMove += _weaponRotateMechanic.RotateWeapon;

            _delayBetweenShotsMechanic.Construct();
            //TODO: Нужно событие перед выстрелом, событие выстрела и событие после выстрела
            

            _shootMechanic.Construct(projectileFactory);
            _shootMechanic.Condition.Append(_weaponReloadMechanic.IsNotReloading);
            _shootMechanic.Condition.Append(_ammoInClipDecreaseMechanic.IsEnoughAmmoToShoot);
            _shootMechanic.Condition.Append(_delayBetweenShotsMechanic.CanShoot);

            if (_isAutomatic)
            {
                _shootInputProvider.OnShootHold += _shootMechanic.OnShoot;
            }
            else
            {
                _shootInputProvider.OnShoot += _shootMechanic.OnShoot;
            }

            _ammoInClipDecreaseMechanic.Construct(_ammoClipStorage);
            _shootMechanic.ShootComplete += _ammoInClipDecreaseMechanic.OnShootComplete;
            _shootMechanic.ShootComplete += _delayBetweenShotsMechanic.OnShoot;
        }

        //TODO: Перетащить в weapon controller
        private void OnEnable()
        {
            if (_shootInputProvider == null) return;
            if(_reloadInputProvider == null) return;
            if(_worldMouseMoveInputProvider == null) return;
            if(_screenMouseMoveInputProvider == null) return;

            _reloadInputProvider.OnReload += _weaponReloadMechanic.OnReload;
            _worldMouseMoveInputProvider.OnWorldMouseMove += _weaponRotateMechanic.RotateWeapon;

            if (_isAutomatic)
            {
                _shootInputProvider.OnShootHold += _shootMechanic.OnShoot;
            }
            else
            {
                _shootInputProvider.OnShoot += _shootMechanic.OnShoot;
            }

            _shootMechanic.ShootComplete += _ammoInClipDecreaseMechanic.OnShootComplete;
            _shootMechanic.ShootComplete += _delayBetweenShotsMechanic.OnShoot;
        }

        private void OnDisable()
        {
            if (_shootInputProvider == null) return;
            if (_reloadInputProvider == null) return;
            if (_worldMouseMoveInputProvider == null) return;
            if (_screenMouseMoveInputProvider == null) return;

            _reloadInputProvider.OnReload -= _weaponReloadMechanic.OnReload;
            _worldMouseMoveInputProvider.OnWorldMouseMove -= _weaponRotateMechanic.RotateWeapon;

            if (_isAutomatic)
            {
                _shootInputProvider.OnShootHold -= _shootMechanic.OnShoot;
            }
            else
            {
                _shootInputProvider.OnShoot -= _shootMechanic.OnShoot;
            }

            _shootMechanic.ShootComplete -= _ammoInClipDecreaseMechanic.OnShootComplete;
            _shootMechanic.ShootComplete -= _delayBetweenShotsMechanic.OnShoot;
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            _weaponSprite = GetComponentInChildren<SpriteRenderer>();
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