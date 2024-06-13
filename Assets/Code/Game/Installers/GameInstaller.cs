using ArenaShooter.AI;
using ArenaShooter.Inputs;
using ArenaShooter.Units.Player;
using ArenaShooter.Weapons;
using ArenaShooter.Weapons.Projectiles;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private AIStateMachineFactory _stateMachineFactory;

        [SerializeField]
        private KeyboardAndMouseInputController _currentInputController;

        [SerializeField]
        private Transform _player;

        [SerializeField]
        private ProjectileFactory _projectileFactory;

        [SerializeField]
        private WeaponFactory _weaponFactory;

        public override void InstallBindings()
        {
            Container.BindInstance(_currentInputController).AsSingle();
            Container.BindInstance(_player).AsSingle();
            Container.BindInstance(_stateMachineFactory).AsSingle();
            Container.BindInstance(_projectileFactory).AsSingle();
            Container.BindInstance(_weaponFactory).AsSingle();

            Container.Bind(typeof(IMoveInputProvider), typeof(IMouseMoveInputProvider),
                typeof(IShootInputProvider), typeof(IReloadInputProvider),
                typeof(IChangeWeaponInputProvider), typeof(IDashInputProvider)).FromInstance(_currentInputController);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _stateMachineFactory = FindObjectOfType<AIStateMachineFactory>();
            _weaponFactory = FindObjectOfType<WeaponFactory>();
            _projectileFactory = FindObjectOfType<ProjectileFactory>();

            _player = FindObjectOfType<PlayerCoordinator>().transform;
        }
#endif
    }
}