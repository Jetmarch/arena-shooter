using ArenaShooter.Components;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    [RequireComponent(typeof(MoveComponent))]
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed;

        private MoveComponent _moveComponent;

        private void Start()
        {
            _moveComponent = GetComponent<MoveComponent>();
        }

        private void FixedUpdate()
        {
            _moveComponent.OnMoveFixedUpdate(transform.right, _moveSpeed);
        }
    }
}