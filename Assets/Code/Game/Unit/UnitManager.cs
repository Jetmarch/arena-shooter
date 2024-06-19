using ArenaShooter.Units.Factories;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    /// <summary>
    /// ”правл€ет жизненным циклом юнитов
    /// </summary>
    public class UnitManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _units = new();
        [SerializeField]
        private List<BaseUnitFactory> _unitFactories = new();

        public event Action<GameObject> UnitCreated;
        public event Action<GameObject> UnitDie;

        [Inject]
        private void Construct(List<BaseUnitFactory> unitFactories)
        {
            _unitFactories = unitFactories;
        }

        public GameObject CreateUnit(UnitType type, Vector3 position, Transform parent)
        {
            var factory = _unitFactories.Find(x => x.Type == type);
            if (factory == null)
            {
                throw new Exception($"Factory for type {type} doesn't found!");
            }
            var unit = factory.CreateUnit(position, parent);
            _units.Add(unit);
            UnitCreated?.Invoke(unit);
            var dieMechanic = unit.GetComponent<UnitDieMechanic>();

            if (dieMechanic == null)
            {
                Debug.LogWarning($"Unit {unit.name} doesn't have UnitDieMechanic!");
                return unit;
            }

            dieMechanic.OnDie += OnUnitDie;

            return unit;
        }

        public void DestroyUnit(GameObject unit)
        {
            var dieMechanic = unit.GetComponent<UnitDieMechanic>();
            if (dieMechanic == null)
            {
                Debug.LogWarning($"Try to destroy unit {unit.name} without UnitDieMechanic! Called Object.Destroy()");

                Destroy(unit);
                return;
            }

            dieMechanic.Die();
        }

        private void OnUnitDie(GameObject unit)
        {
            _units.Remove(unit);
            UnitDie?.Invoke(unit);
        }
    }
}