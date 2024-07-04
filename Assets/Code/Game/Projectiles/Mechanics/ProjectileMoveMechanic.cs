using ArenaShooter.Components;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Projectiles
{
    [RequireComponent(typeof(Move2DComponent))]
    public sealed class ProjectileMoveMechanic : MonoBehaviour, IGameFixedUpdateListener
    {
        private Move2DComponent _moveComponent;

        [Inject]
        public void Construct(Move2DComponent moveComponent)
        {
            _moveComponent = moveComponent;
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
            _moveComponent.Move(transform.right);
        }
    }
}