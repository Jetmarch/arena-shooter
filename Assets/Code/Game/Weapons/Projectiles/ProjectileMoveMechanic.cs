using ArenaShooter.Components;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    [RequireComponent(typeof(Move2DComponent))]
    public sealed class ProjectileMoveMechanic : MonoBehaviour
    {
        [SerializeField]
        private ProjectileConditionContainer _conditionContainer;

        private Move2DComponent _moveComponent;

        private void Start()
        {
            _moveComponent = GetComponent<Move2DComponent>();
            _conditionContainer = GetComponent<ProjectileConditionContainer>();
        }

        private void FixedUpdate()
        {
            _moveComponent.Move(transform.right, _conditionContainer.MoveSpeed);
        }
    }
}