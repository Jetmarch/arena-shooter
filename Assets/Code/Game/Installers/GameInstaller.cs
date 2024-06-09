using ArenaShooter.Inputs;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private BaseInputController _currentInputController;

        public override void InstallBindings()
        {
            Container.BindInstance(_currentInputController).AsSingle();
        }
    }
}