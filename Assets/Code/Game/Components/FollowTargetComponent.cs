using UnityEngine;

namespace ArenaShooter.Components
{
    public sealed class FollowTargetComponent : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _offset;
        [SerializeField]
        private float _followSpeed;

        public void Follow(Vector3 targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition + _offset, _followSpeed);
        }
    }
}