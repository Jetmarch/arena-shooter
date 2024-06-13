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
        private UnitMoveController _moveController;
        [SerializeField]
        private UnitDashController _dashController;
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
            _moveController = GetComponent<UnitMoveController>();
            _dashController = GetComponent<UnitDashController>();
            _weaponSetController = GetComponent<WeaponSetController>();
        }
#endif
    }
}