using ArenaShooter.Units.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Factories
{
    public class UnitFactory : MonoBehaviour, IPlayerProvider
    {
        [SerializeField]
        private List<UnitFactoryData> _units;
        private DiContainer _container;

        private PlayerFacade _player;

        public event Action<PlayerFacade> OnPlayerCreated;

        public PlayerFacade Player => _player;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        [Serializable]
        public class UnitFactoryData
        {
            public UnitType UnitType;
            public GameObject UnitPrefab;
        }

        public GameObject CreateUnit(UnitType type, Vector3 position, Transform parent)
        {
            var unitPrefab = _units.Find(x => x.UnitType == type).UnitPrefab;
            if (unitPrefab == null)
            {
                throw new Exception($"UnitFactory: type {type} does not contain unit prefab!");
            }

            var unit = _container.InstantiatePrefab(unitPrefab, position, unitPrefab.transform.rotation, parent);
            if(type == UnitType.Player)
            {
                _player = unit.GetComponent<PlayerFacade>();
                if(_player == null)
                {
                    throw new Exception("UnitFactory: player object does not contain PlayerFacade component!");
                }
                OnPlayerCreated?.Invoke(_player);
            }
            return unit;
        }
    }
}