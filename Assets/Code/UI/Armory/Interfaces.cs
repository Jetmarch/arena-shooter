using ArenaShooter.Artefacts;
using ArenaShooter.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.UI
{
    public interface IItemPresenter
    {
        Sprite Sprite { get; }
        string Name { get; }
        string Description { get; }
    }

    public interface IWeaponPresenter : IItemPresenter
    {
        WeaponType WeaponType { get; }
        void ChooseWeapon(WeaponType weaponType);
    }

    public interface IArtefactPresenter : IItemPresenter
    {
        ArtefactType ArtefactType { get; }
        void ChooseArtefact(ArtefactType artefactType);
    }

    public interface IWeaponContainerPresenter
    {
        List<IWeaponPresenter> Weapons { get; }
    }

    public interface IArmoryContainerModel
    {
        List<WeaponConfig> Weapons { get; }
        List<ArtefactConfig> Artefacts { get; }
    }

}