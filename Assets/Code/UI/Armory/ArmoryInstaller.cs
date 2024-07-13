using System.Collections.Generic;
using TMPro;
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

        [SerializeField]
        private TextMeshProUGUI _itemDescriptionLabel;

        public override void InstallBindings()
        {
            InstallWeapons();
            InstallArtefacts();
        }

        private void InstallWeapons()
        {
            var weaponViews = new List<ItemView>();
            var itemContainer = new ItemContainerPresenter(weaponViews);
            foreach (var weapon in _armoryConfig.Weapons)
            {
                var weaponView = _weaponViewContainer.CreateWeaponView();
                Container.BindInterfacesAndSelfTo<WeaponPresenter>().AsCached().WithArguments(weapon, weaponView, itemContainer, _itemDescriptionLabel);
                weaponViews.Add(weaponView);
            }
        }

        private void InstallArtefacts()
        {
            var artefactViews = new List<ItemView>();
            var itemContainer = new ItemContainerPresenter(artefactViews);
            foreach (var artefact in _armoryConfig.Artefacts)
            {
                var artefactView = _artefactViewContainer.CreateArtefactView();
                Container.BindInterfacesAndSelfTo<ArtefactPresenter>().AsCached().WithArguments(artefact, artefactView, itemContainer, _itemDescriptionLabel);
                artefactViews.Add(artefactView);
            }
        }
    }
}