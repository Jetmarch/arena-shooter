using ArenaShooter.Components;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    [RequireComponent(typeof(Move2DComponent))]
    public sealed class ProjectileMoveMechanic : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField]
        private ProjectileConditionContainer _conditionContainer;

        private Move2DComponent _moveComponent;

        private void Start()
        {
            _moveComponent = GetComponent<Move2DComponent>();
            _conditionContainer = GetComponent<ProjectileConditionContainer>();
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }

        public void OnFixedUpdate(float delta)
        {
            _moveComponent.Move(transform.right, _conditionContainer.MoveSpeed);
        }
    }
}