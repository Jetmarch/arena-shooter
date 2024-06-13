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
        private UnitMoveMechanic _moveController;
        [SerializeField]
        private AIInputController _inputController;


        [Inject]
        private void Construct(Transform target)
        {
            _moveController.Constuct(_inputController);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _moveController = GetComponent<UnitMoveMechanic>();
            _inputController = GetComponent<AIInputController>();
        }
#endif
    }
}