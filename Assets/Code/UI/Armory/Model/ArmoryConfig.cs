using ArenaShooter.Artefacts;
using ArenaShooter.Weapons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.UI
{
    [CreateAssetMenu(fileName = "ArmoryConfig", menuName = "SO/Configs/ArmoryConfig")]
    public class ArmoryConfig : ScriptableObject, IArmoryContainerModel
    {
        [SerializeField]
        private List<WeaponConfig> _weapons;
        [SerializeField]
        private List<ArtefactConfig> _artefacts;

        private List<WeaponConfig> GetWeapons()
        {
            var weapons = new List<WeaponConfig>();
            weapons.AddRange(_weapons);
            return weapons;
        }

        private List<ArtefactConfig> GetArtefacts()
        {
            var artefacts = new List<ArtefactConfig>();
            artefacts.AddRange(_artefacts);
            return artefacts;
        }

        public List<WeaponConfig> Weapons => GetWeapons();

        public List<ArtefactConfig> Artefacts => GetArtefacts();

    }

    [Serializable]
    public class WeaponConfig 
    {
        [SerializeField]
        private Sprite _sprite;
        [SerializeField]
        private string _name;
        [SerializeField]
        private string _description;
        [SerializeField]
        private WeaponType _weaponType;

        public Sprite Sprite => _sprite;
        public string Name => _name;
        public string Description => _description;
        public WeaponType WeaponType => _weaponType;
    }

    [Serializable]
    public class ArtefactConfig 
    {
        [SerializeField]
        private Sprite _sprite;
        [SerializeField]
        private string _name;
        [SerializeField]
        private string _description;
        [SerializeField]
        private ArtefactType _artefactType;

        public Sprite Sprite => _sprite;
        public string Name => _name;
        public string Description => _description;
        public ArtefactType ArtefactType => _artefactType;
    }
}