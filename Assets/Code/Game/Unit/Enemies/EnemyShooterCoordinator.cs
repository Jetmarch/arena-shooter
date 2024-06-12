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
        private UnitMoveController _moveController;
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
            _moveController = GetComponent<UnitMoveController>();
            _inputController = GetComponent<AIInputController>();
        }
#endif
    }
}