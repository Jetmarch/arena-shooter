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

        private IShootInputProvider _shootInputProvider;
        private IMouseMoveInputProvider _mouseMoveInputProvider;
        private IReloadInputProvider _reloadInputProvider;
        private ProjectileFactory _projectileFactory;

        [Inject]
        private void Construct(IShootInputProvider shootInputProvider, IMouseMoveInputProvider mouseMoveInputProvider, IReloadInputProvider reloadInputProvider, ProjectileFactory projectileFactory)
        {
            _shootInputProvider = shootInputProvider;
            _projectileFactory = projectileFactory;
            _mouseMoveInputProvider = mouseMoveInputProvider;
            _reloadInputProvider = reloadInputProvider;
        }

        public GameObject CreateWeapon(WeaponType type, Vector3 position, Transform parent)
        {
            var weapon = _weapons.Find(x => x.Type == type).WeaponPrefab;
            if (weapon == null)
            {
                throw new Exception($"WeaponFactory: Type {type} does not contain prefab object!");
            }

            var createdWeapon = Instantiate(weapon, position, weapon.transform.rotation, parent);
            createdWeapon.GetComponent<WeaponInstaller>().Construct(_shootInputProvider, _mouseMoveInputProvider, _reloadInputProvider, _projectileFactory);
            return createdWeapon;
        }
    }
}