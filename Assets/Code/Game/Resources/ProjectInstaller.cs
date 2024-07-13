using ArenaShooter.Artefacts;
using ArenaShooter.Weapons;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ArtefactSet>().AsSingle();
        Container.Bind<WeaponSet>().AsSingle();
    }
}