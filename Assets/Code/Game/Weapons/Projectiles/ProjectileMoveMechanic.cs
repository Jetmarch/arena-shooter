using ArenaShooter.Components;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    [RequireComponent(typeof(Move2DComponent))]
    public sealed class ProjectileMoveMechanic : MonoBehaviour, IGameFixedUpdateListener
    {
        private Move2DComponent _moveComponent;

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