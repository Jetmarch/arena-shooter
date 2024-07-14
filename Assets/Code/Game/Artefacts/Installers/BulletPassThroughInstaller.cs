using Zenject;

namespace ArenaShooter.Artefacts
{
    public class BulletPassThroughInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BulletsPassThroughMechanic>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BulletsPassThroughController>().AsSingle().NonLazy();
        }
    }
}