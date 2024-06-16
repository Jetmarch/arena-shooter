using ArenaShooter.Inputs;
using ArenaShooter.Units.Player;
using ArenaShooter.Weapons;
using System;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Factories
{
    public class PlayerUnitFactory : BaseUnitFactory
    {
        private IMoveInputProvider _moveInputProvider;
        private IDashInputProvider _dashInputProvider;
        private IChangeWeaponInputProvider _changeWeaponInputProvider;
        private PlayerWeaponFactory _playerWeaponFactory;

        [Inject]
        private void Construct(IMoveInputProvider moveInputProvider, IDashInputProvider dashInputProvider, IChangeWeaponInputProvider changeWeaponInputProvider, PlayerWeaponFactory playerWeaponFactory)
        {
            _moveInputProvider = moveInputProvider;
            _dashInputProvider = dashInputProvider;
            _changeWeaponInputProvider = changeWeaponInputProvider;
            _playerWeaponFactory = playerWeaponFactory;
        }

        public override GameObject CreateUnit(Vector3 position, Transform parent)
        {
            var player = Instantiate(_unitPrefab, position, _unitPrefab.transform.rotation, parent);
            var installer = player.GetComponent<PlayerInstaller>();
            if(installer == null)
            {
                throw new Exception("Player prefab doesn't have PlayerInstaller component!");
            }

            installer.Construct(_moveInputProvider, _dashInputProvider, _changeWeaponInputProvider, _playerWeaponFactory);
            return player;
        }
    }
}