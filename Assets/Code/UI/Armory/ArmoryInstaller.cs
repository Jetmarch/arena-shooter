using ArenaShooter.Artefacts;
using ArenaShooter.Weapons;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.UI
{
    public class ArmoryInstaller : MonoInstaller
    {
        [SerializeField]
        private ArmoryConfig _armoryConfig;

        [SerializeField]
        private WeaponViewContainer _weaponViewContainer;

        [SerializeField]
        private ArtefactViewContainer _artefactViewContainer;

        public override void InstallBindings()
        {
            Container.Bind<WeaponSet>().AsSingle();
            Container.Bind<ArtefactSet>().AsSingle();

            InstallWeapons();
            InstallArtefacts();
        }

        private void InstallWeapons()
        {
            foreach(var weapon in _armoryConfig.Weapons)
            {
                var weaponView = _weaponViewContainer.CreateWeaponView();
                Container.BindInterfacesAndSelfTo<WeaponPresenter>().AsCached().WithArguments(weapon, weaponView);
            }
        }

        private void InstallArtefacts()
        {
            foreach (var artefact in _armoryConfig.Artefacts)
            {
                var artefactView = _artefactViewContainer.CreateArtefactView();
                Container.BindInterfacesAndSelfTo<ArtefactPresenter>().AsCached().WithArguments(artefact, artefactView);
            }
        }
    }
}