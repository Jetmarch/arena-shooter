using Zenject;

namespace ArenaShooter.Artefacts
{
    public class DropStunProjectileOnReloadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<DropStunProjectileMechanic>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<DropStunProjectileOnReloadController>().AsSingle().NonLazy();
        }
    }
}