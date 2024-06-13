using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Units.Player
{
    public class PlayerCoordinator : MonoBehaviour
    {
        [SerializeField]
        private UnitConditionContainer _conditionContainer;
        [SerializeField]
        private UnitMoveMechanic _moveController;
        [SerializeField]
        private UnitDashMechanic _dashController;
        [SerializeField]
        private WeaponSetController _weaponSetController;


        [Inject]
        private void Construct(IMoveInputProvider inputController)
        {
            _moveController.Constuct(inputController);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _conditionContainer = GetComponent<UnitConditionContainer>();
            _moveController = GetComponent<UnitMoveMechanic>();
            _dashController = GetComponent<UnitDashMechanic>();
            _weaponSetController = GetComponent<WeaponSetController>();
        }
#endif
    }
}