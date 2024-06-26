using Zenject;

namespace ArenaShooter.UI
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<HealthBarController>().AsSingle();
        }
    }
}