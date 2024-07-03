using ArenaShooter.Inputs;
using ArenaShooter.Weapons.Projectiles;
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

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        public WeaponFacade CreateWeapon(WeaponType type, Vector3 position, Transform parent)
        {
            var weapon = _weapons.Find(x => x.Type == type).WeaponPrefab;
            if (weapon == null)
            {
                throw new Exception($"WeaponFactory: Type {type} does not contain prefab object!");
            }

            var createdWeapon = _container.InstantiatePrefab(weapon, position, weapon.transform.rotation, parent);
            return createdWeapon.GetComponent<WeaponFacade>();
        }
    }
}