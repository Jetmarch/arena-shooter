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
        private IScreenMouseMoveInputProvider _screenMouseMoveInputProvider;
        private IWorldMouseMoveInputProvider _worldMouseMoveInputProvider;
        private IReloadInputProvider _reloadInputProvider;
        private ProjectileFactory _projectileFactory;

        [Inject]
        private void Construct(IShootInputProvider shootInputProvider, IScreenMouseMoveInputProvider mouseMoveInputProvider, IWorldMouseMoveInputProvider worldMouseMoveInputProvider, IReloadInputProvider reloadInputProvider, ProjectileFactory projectileFactory)
        {
            _shootInputProvider = shootInputProvider;
            _projectileFactory = projectileFactory;
            _screenMouseMoveInputProvider = mouseMoveInputProvider;
            _worldMouseMoveInputProvider = worldMouseMoveInputProvider;
            _reloadInputProvider = reloadInputProvider;
        }

        public WeaponFacade CreateWeapon(WeaponType type, Vector3 position, Transform parent)
        {
            var weapon = _weapons.Find(x => x.Type == type).WeaponPrefab;
            if (weapon == null)
            {
                throw new Exception($"WeaponFactory: Type {type} does not contain prefab object!");
            }

            var createdWeapon = Instantiate(weapon, position, weapon.transform.rotation, parent);
            createdWeapon.GetComponent<BaseWeaponInstaller>().Construct(_shootInputProvider, _screenMouseMoveInputProvider, _worldMouseMoveInputProvider, _reloadInputProvider, _projectileFactory);
            return createdWeapon.GetComponent<WeaponFacade>();
        }
    }
}