using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Factories
{
    public class UnitFactory : MonoBehaviour
    {
        [SerializeField]
        private List<UnitFactoryData> _units;
        private DiContainer _container;


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

            return unit;
        }
    }
}