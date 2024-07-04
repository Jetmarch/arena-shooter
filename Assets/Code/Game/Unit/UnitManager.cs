using ArenaShooter.Components;
using ArenaShooter.Units.Factories;
using ArenaShooter.Units.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units
{
    /// <summary>
    /// ”правл€ет жизненным циклом юнитов
    /// </summary>
    public class UnitManager : MonoBehaviour, IPlayerProvider
    {
        [SerializeField]
        private List<GameObject> _units;
        [SerializeField]
        private UnitFactory _unitFactory;

        private PlayerFacade _playerFacade;
        public PlayerFacade Player { get { return _playerFacade; } }

        public event Action<GameObject> UnitCreated;
        public event Action<GameObject> UnitDie;
        public event Action<PlayerFacade> OnPlayerCreated;
        public event Action<PlayerFacade> OnPlayerDied;

        [Inject]
        private void Construct(UnitFactory unitFactories)
        {
            _unitFactory = unitFactories;
        }

        public GameObject CreateUnit(UnitType type, Vector3 position, Transform parent)
        {
            var unit = _unitFactory.CreateUnit(type, position, parent);
            _units.Add(unit);
            UnitCreated?.Invoke(unit);
            var dieMechanic = unit.GetComponent<UnitDieMechanic>();

            if (dieMechanic == null)
            {
                Debug.LogWarning($"Unit {unit.name} doesn't have UnitDieMechanic!");
                return unit;
            }

            dieMechanic.OnDie += OnUnitDie;

            if (type == UnitType.Player)
            {
                _playerFacade = unit.GetComponent<PlayerFacade>();
                if (_playerFacade == null)
                {
                    throw new Exception("UnitManager: player object does not contain PlayerFacade component!");
                }
                OnPlayerCreated?.Invoke(_playerFacade);
            }

            return unit;
        }

        public void DestroyUnit(GameObject unit)
        {
            var healthComponent = unit.GetComponent<HealthComponent>();
            if (healthComponent == null)
            {
                Debug.LogWarning($"Try to destroy unit {unit.name} without HealthComponent! Called Object.Destroy()");

                Destroy(unit);
                return;
            }

            healthComponent.SetCurrentHealth(healthComponent.MinHealth);
        }

        private void OnUnitDie(GameObject unit)
        {
            UnitDie?.Invoke(unit);
            _units.Remove(unit);

            var playerFacade = unit.GetComponent<PlayerFacade>();
            if (playerFacade != null)
            {
                OnPlayerDied?.Invoke(playerFacade);
            }
        }
    }
}