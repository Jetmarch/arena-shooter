using ArenaShooter.Artefacts;
using ArenaShooter.Audio;
using ArenaShooter.CameraScripts;
using ArenaShooter.Components;
using ArenaShooter.Unit;
using ArenaShooter.Units.Enemies;
using ArenaShooter.Weapons;
using System.Collections;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Move2DComponent))]
    [RequireComponent(typeof(UnitDashMechanic))]
    [RequireComponent(typeof(WeaponChangeMechanic))]
    [RequireComponent(typeof(WeaponsStorage))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(UnitTemporaryInvulnerableMechanic))]
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private Move2DComponent _moveComponent;
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        [SerializeField]
        private WeaponChangeMechanic _weaponChangeMechanic;
        [SerializeField]
        private WeaponsStorage _weaponStorage;
        [SerializeField]
        private UnitDashMechanic _dashMechanic;

        [SerializeField]
        private Transform _weaponListParent;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private SpriteFlashMechanic _flashMechanic;

        [SerializeField]
        private HealthComponent _healthComponent;
        [SerializeField]
        private UnitTemporaryInvulnerableMechanic _unitTemporaryInvulnerableMechanic;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private CameraShakeData _shakeCameraDataOnHit;

        [SerializeField]
        private ParticleSystem _footstepEffect;

        [SerializeField]
        private string _hitSoundName;
        [SerializeField]
        private string _changeWeaponSoundName;

        [Inject]
        private WeaponSet _weaponSet;
        [Inject]
        private ArtefactSet _artefactSet;
        [Inject]
        private PlayerWeaponFactory _weaponFactory;
        [Inject]
        private ArtefactFactory _artefactFactory;

        public override void InstallBindings()
        {
            ConstructComponents();

            BindComponents();

            BindMechanics();

            BindMechanicsControllers();

            BindAudio();

            AppendConditions();
        }

        private void ConstructComponents()
        {
            _healthComponent.Construct();
            _moveComponent.Construct(_rigidbody2D);
            _weaponChangeMechanic.Construct(_weaponStorage);
            _dashMechanic.Construct(_moveComponent);
            _flashMechanic.Construct(_spriteRenderer);
        }

        private void BindComponents()
        {
            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<HealthComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<UnitDieMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<WeaponsStorage>().FromInstance(_weaponStorage).AsSingle();
            Container.Bind<SpriteRenderer>().FromInstance(_spriteRenderer).AsSingle();
            Container.Bind<AudioSource>().FromInstance(_audioSource).AsSingle();
            Container.Bind<AudioComponent>().AsSingle().NonLazy();
        }

        private void BindMechanics()
        {
            Container.Bind<UnitTemporaryInvulnerableMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<WeaponChangeMechanic>().FromInstance(_weaponChangeMechanic).AsSingle();
            Container.Bind<UnitDashMechanic>().FromInstance(_dashMechanic).AsSingle();
            Container.Bind<SpriteFlashMechanic>().FromInstance(_flashMechanic).AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponRotateMechanic>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitFootstepEffectMechanic>().AsSingle().WithArguments(_footstepEffect).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerWeaponGiver>().AsSingle().NonLazy();
        }

        private void BindMechanicsControllers()
        {
            Container.BindInterfacesAndSelfTo<UnitMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitWeaponChangeController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDashController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDieController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpriteFlashOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TemporaryInvulnerabilityOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<WeaponRotateController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CameraShakeOnPlayerHitController>().AsSingle().WithArguments(_shakeCameraDataOnHit).NonLazy();
            Container.BindInterfacesAndSelfTo<UnitFootstepEffectController>().AsSingle().NonLazy();
        }

        private void BindAudio()
        {
            Container.BindInterfacesAndSelfTo<HitSoundController>().AsSingle().WithArguments(_hitSoundName).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerChangeWeaponSoundController>().AsSingle().WithArguments(_changeWeaponSoundName).NonLazy();
        }

        private void AppendConditions()
        {
            _moveComponent.Condition.Append(_dashMechanic.IsNotDashing);
            _healthComponent.Condition.Append(_unitTemporaryInvulnerableMechanic.IsNotInvulnerable);
        }

        private void SetWeaponsOwner()
        {
            foreach (var weapon in _weaponStorage.Weapons)
            {
                weapon.WeaponShootMechanic.SetOwner(gameObject);
            }
        }

        public override void Start()
        {
            StartCoroutine(DelayedWeaponAndArtefactInstall());
        }

        private IEnumerator DelayedWeaponAndArtefactInstall()
        {
            yield return new WaitForSeconds(0.5f);
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(_weaponSet.PrimaryWeapon));
            _weaponStorage.AddWeapon(_weaponFactory.CreateWeapon(_weaponSet.SecondaryWeapon));
            _weaponChangeMechanic.OnChangeWeaponUp();
            SetWeaponsOwner();

            _artefactFactory.CreateArtefact(_artefactSet.PrimaryArtefact);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _moveComponent = GetComponent<Move2DComponent>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _weaponChangeMechanic = GetComponent<WeaponChangeMechanic>();
            _weaponStorage = GetComponent<WeaponsStorage>();
            _dashMechanic = GetComponent<UnitDashMechanic>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _flashMechanic = GetComponentInChildren<SpriteFlashMechanic>();
            _unitTemporaryInvulnerableMechanic = GetComponent<UnitTemporaryInvulnerableMechanic>();
            _healthComponent = GetComponentInChildren<HealthComponent>();
            _audioSource = GetComponentInChildren<AudioSource>();
            _footstepEffect = GetComponentInChildren<ParticleSystem>();
        }
#endif
    }

}