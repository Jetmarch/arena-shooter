using ArenaShooter.Units.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// Собирает оружие для игрока
    /// TODO: Собирать оружие для любого юнита
    /// </summary>
    public class PlayerWeaponFactory : MonoBehaviour
    {
        [Serializable]
        public struct WeaponFactoryData
        {
            public WeaponType Type;
            public GameObject WeaponPrefab;
        }

        [SerializeField]
        private List<WeaponFactoryData> _weapons;

        private DiContainer _container;
        private IPlayerProvider _playerProvider;

        [Inject]
        private void Construct(DiContainer container, IPlayerProvider playerProvider)
        {
            _container = container;
            _playerProvider = playerProvider;
        }

        public WeaponFacade CreateWeapon(WeaponType type)
        {
            if (_playerProvider.Player == null)
            {
                throw new Exception($"WeaponFactory: Player is not created yet!");
            }

            var weapon = _weapons.Find(x => x.Type == type).WeaponPrefab;
            if (weapon == null)
            {
                throw new Exception($"WeaponFactory: Type {type} does not contain prefab object!");
            }

            var createdWeapon = _container.InstantiatePrefab(weapon, _playerProvider.Player.WeaponList.position, weapon.transform.rotation, _playerProvider.Player.WeaponList);
            return createdWeapon.GetComponent<WeaponFacade>();
        }
    }
}