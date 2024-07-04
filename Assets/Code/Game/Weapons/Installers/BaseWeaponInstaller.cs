using ArenaShooter.Audio;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    [RequireComponent(typeof(AmmoClipStorage))]
    [RequireComponent(typeof(BaseWeaponShootMechanic))]
    [RequireComponent(typeof(WeaponReloadMechanic))]
    [RequireComponent(typeof(WeaponDelayBetweenShotsMechanic))]
    public class BaseWeaponInstaller : MonoInstaller
    {
        [SerializeField]
        private AmmoClipStorage _ammoClipStorage;
        [SerializeField]
        private BaseWeaponShootMechanic _shootMechanic;

        [SerializeField]
        private WeaponReloadMechanic _weaponReloadMechanic;

        [SerializeField]
        private WeaponDelayBetweenShotsMechanic _delayBetweenShotsMechanic;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private WeaponFacade _weaponFacade;

        [SerializeField]
        private string _shootSoundName;

        [SerializeField]
        private int _amountOfAmmoOnOneShot = 1;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private ParticleSystem _gunShellParticles;

        [SerializeField]
        //Сделал для удобства одно условие здесь,
        //так как это единственная разница между автоматами и остальным оружием
        //на текущий момент
        private bool _isAutomatic;

        //private IShootInputProvider _shootInputProvider;
        //private IScreenMouseMoveInputProvider _screenMouseMoveInputProvider;
        //private IWorldMouseMoveInputProvider _worldMouseMoveInputProvider;
        //private IReloadInputProvider _reloadInputProvider;

        public override void InstallBindings()
        {
            ConstructComponents();
            BindMechanics();
            BindComponents();
            BindControllers();
            AppendConditions();
        }

        //TODO: Продумать автоматическую подвязку оружия к носителю
        //public void Construct(IShootInputProvider shootInputProvider, IScreenMouseMoveInputProvider mouseMoveInputProvider,
        //    IWorldMouseMoveInputProvider worldMouseMoveProvider, IReloadInputProvider reloadInputProvider)
        //{
        //    _shootInputProvider = shootInputProvider;
        //    _screenMouseMoveInputProvider = mouseMoveInputProvider;
        //    _worldMouseMoveInputProvider = worldMouseMoveProvider;
        //    _reloadInputProvider = reloadInputProvider;
        //}

        private void ConstructComponents()
        {
            _weaponReloadMechanic.Construct(_ammoClipStorage);
            _delayBetweenShotsMechanic.Construct();
        }

        private void BindComponents()
        {
            Container.Bind<SpriteRenderer>().FromInstance(_spriteRenderer).AsSingle();
            Container.Bind<AudioSource>().FromInstance(_audioSource).AsSingle();
            Container.Bind<AudioComponent>().AsSingle().NonLazy();
            Container.Bind<string>().FromInstance(_shootSoundName).AsSingle();
            Container.Bind<ParticleSystem>().FromInstance(_gunShellParticles).AsSingle();
        }

        private void BindMechanics()
        {
            Container.Bind<WeaponReloadMechanic>().FromInstance(_weaponReloadMechanic).AsSingle();
            Container.Bind<WeaponDelayBetweenShotsMechanic>().FromInstance(_delayBetweenShotsMechanic).AsSingle();
            Container.Bind<BaseWeaponShootMechanic>().FromInstance(_shootMechanic).AsSingle();
            Container.Bind<WeaponFlipSpriteMechanic>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AmmoInClipDecreaseMechanic>().AsSingle().WithArguments(_amountOfAmmoOnOneShot, _ammoClipStorage).NonLazy();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<WeaponSpriteFlipController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<WeaponReloadController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AmmoInClipDecreaseController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DelayBetweenShotsController>().AsSingle().NonLazy();

            if (_isAutomatic)
            {
                Container.BindInterfacesAndSelfTo<WeaponShootHoldController>().AsSingle().NonLazy();
            }
            else
            {
                Container.BindInterfacesAndSelfTo<WeaponShootController>().AsSingle().NonLazy();
            }

            Container.BindInterfacesAndSelfTo<ShootSoundController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GunShellParticlesController>().AsSingle().NonLazy();
        }

        private void AppendConditions()
        {
            //TODO: Придумать как применять conditions
            //Возможно стоит сделать обертку
            //_shootMechanic.Condition.Append(_weaponReloadMechanic.IsNotReloading);
            ////_shootMechanic.Condition.Append(_ammoInClipDecreaseMechanic.IsEnoughAmmoToShoot);
            //_shootMechanic.Condition.Append(_delayBetweenShotsMechanic.CanShoot);
            //_shootMechanic.Condition.Append(IsGameObjectActive);
            //_weaponReloadMechanic.Condition.Append(IsGameObjectActive);
            Container.Bind<WeaponFacade>().FromInstance(_weaponFacade).AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponConditionInstaller>().AsSingle().NonLazy();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _audioSource = GetComponentInChildren<AudioSource>();
            _weaponFacade = GetComponent<WeaponFacade>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _ammoClipStorage = GetComponent<AmmoClipStorage>();
            _shootMechanic = GetComponent<BaseWeaponShootMechanic>();
            _weaponReloadMechanic = GetComponent<WeaponReloadMechanic>();
            _delayBetweenShotsMechanic = GetComponent<WeaponDelayBetweenShotsMechanic>();
            _gunShellParticles = GetComponentInChildren<ParticleSystem>();
        }
#endif
    }

    public class WeaponConditionInstaller : IInitializable
    {
        private BaseWeaponShootMechanic _shootMechanic;
        private WeaponReloadMechanic _weaponReloadMechanic;
        private AmmoInClipDecreaseMechanic _ammoInClipDecreaseMechanic;
        private WeaponDelayBetweenShotsMechanic _delayBetweenShotsMechanic;
        private WeaponFacade _weaponFacade;

        public WeaponConditionInstaller(BaseWeaponShootMechanic shootMechanic, WeaponReloadMechanic weaponReloadMechanic, AmmoInClipDecreaseMechanic ammoInClipDecreaseMechanic, WeaponDelayBetweenShotsMechanic delayBetweenShotsMechanic, WeaponFacade weaponFacade)
        {
            _shootMechanic = shootMechanic;
            _weaponReloadMechanic = weaponReloadMechanic;
            _ammoInClipDecreaseMechanic = ammoInClipDecreaseMechanic;
            _delayBetweenShotsMechanic = delayBetweenShotsMechanic;
            _weaponFacade = weaponFacade;
        }

        public void Initialize()
        {
            _shootMechanic.Condition.Append(_weaponReloadMechanic.IsNotReloading);
            _shootMechanic.Condition.Append(_ammoInClipDecreaseMechanic.IsEnoughAmmoToShoot);
            _shootMechanic.Condition.Append(_delayBetweenShotsMechanic.CanShoot);
            _shootMechanic.Condition.Append(IsGameObjectActive);
            _weaponReloadMechanic.Condition.Append(IsGameObjectActive);
        }

        private bool IsGameObjectActive()
        {
            return _weaponFacade.gameObject.activeSelf;
        }
    }
}