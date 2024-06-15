using ArenaShooter.Components;
using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Enemies
{
    public class EnemyShooterCoordinator : MonoBehaviour
    {
        [SerializeField]
        private Move2DComponent _moveComponent;
        [SerializeField]
        private UnitMoveMechanic _moveController;
        [SerializeField]
        private AIInputController _inputController;


        //TODO: Конструировать через фабрику
        [Inject]
        private void Construct(Transform target)
        {
            _moveController.Constuct(_inputController, _moveComponent);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _moveComponent = GetComponent<Move2DComponent>();
            _moveController = GetComponent<UnitMoveMechanic>();
            _inputController = GetComponent<AIInputController>();
        }
#endif
    }
}