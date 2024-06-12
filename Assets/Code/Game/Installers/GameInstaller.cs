using ArenaShooter.AI;
using ArenaShooter.Inputs;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private AIStateMachineFactory _stateMachineFactory;

        [SerializeField]
        private BaseInputController _currentInputController;

        [SerializeField]
        private Transform _player;

        public override void InstallBindings()
        {
            Container.BindInstance(_currentInputController).AsSingle();
            Container.BindInstance(_player).AsSingle();
            Container.BindInstance(_stateMachineFactory).AsSingle();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _stateMachineFactory = FindObjectOfType<AIStateMachineFactory>();
        }
#endif
    }
}