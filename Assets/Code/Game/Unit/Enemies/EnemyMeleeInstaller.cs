using ArenaShooter.AI;
using ArenaShooter.Components;
using ArenaShooter.Components.Triggers;
using ArenaShooter.Inputs;
using ArenaShooter.Mechanics;
using ArenaShooter.Units.Player;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Enemies
{
    public class EnemyMeleeInstaller : MonoInstaller
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private SpriteFlashMechanic _spriteFlashMechanic;

        [SerializeField]
        private HealthComponent _healthComponent;
        [SerializeField]
        private UnitTemporaryInvulnerableMechanic _unitTemporaryInvulnerableMechanic;

        [SerializeField]
        private PlayerScannerComponent _playerScanner;
        [SerializeField]
        private Trigger2DComponent _damageTrigger;

        [SerializeField]
        private MeleeAIBrain _meleeAIBrain;

        [SerializeField]
        private AIInputController _inputController;

        public override void InstallBindings()
        {
            _spriteFlashMechanic.Construct(_spriteRenderer);
            _meleeAIBrain.Construct(_inputController, _playerScanner);

            Container.Bind<MeleeAIBrain>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<UnitDieMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<HealthComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Move2DComponent>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<UnitTemporaryInvulnerableMechanic>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<Trigger2DComponent>().FromInstance(_damageTrigger).AsSingle();
            Container.BindInterfacesAndSelfTo<DamageMechanic>().FromComponentsInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<AIInputController>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<SpriteFlashMechanic>().FromInstance(_spriteFlashMechanic).AsSingle();
            Container.Bind<SpriteRenderer>().FromInstance(_spriteRenderer).AsSingle();

            Container.BindInterfacesAndSelfTo<UnitMoveController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UnitDieController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpriteFlashOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TemporaryInvulnerabilityOnHitController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EnemyMeleeChangeSpeedController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ImpactController>().AsSingle().NonLazy();

            _healthComponent.Condition.Append(_unitTemporaryInvulnerableMechanic.IsNotInvulnerable);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteFlashMechanic = GetComponentInChildren<SpriteFlashMechanic>();
            _unitTemporaryInvulnerableMechanic = GetComponent<UnitTemporaryInvulnerableMechanic>();
            _healthComponent = GetComponentInChildren<HealthComponent>();
            _inputController = GetComponent<AIInputController>();
            _playerScanner = GetComponentInChildren<PlayerScannerComponent>();
            _meleeAIBrain = GetComponent<MeleeAIBrain>();
        }
#endif
    }
}