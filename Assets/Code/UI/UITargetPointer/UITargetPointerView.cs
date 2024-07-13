using UnityEngine;

namespace ArenaShooter.UI
{
    public class UITargetPointerView : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Vector3 _offsetScreenBorder;

        [SerializeField]
        private Vector3 _offsetOnTarget;

        [SerializeField]
        private PointerView _pointer;

        public void Setup(Transform target, Camera camera)
        {
            _target = target;
            _camera = camera;
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }

        public void OnUpdate(float delta)
        {
            if (_target == null) return;

            var targetScreenPosition = _camera.WorldToScreenPoint(_target.position);
            if (IsTargetOnScreen(targetScreenPosition))
            {
                _pointer.SetPosition(targetScreenPosition + _offsetOnTarget);
                _pointer.Activate();
            }
            else
            {
                _pointer.Deactivate();
                _pointer.SetPosition(ClampToScreenPosition(targetScreenPosition));
            }
        }

        private bool IsTargetOnScreen(Vector3 targetPosition)
        {
            if (targetPosition.x > Screen.width || targetPosition.x < 0) return false;
            if (targetPosition.y > Screen.height || targetPosition.y < 0) return false;
            return true;
        }

        private Vector3 ClampToScreenPosition(Vector3 position)
        {
            float x = Mathf.Clamp(position.x, _offsetScreenBorder.x, Screen.width - _offsetScreenBorder.x);
            float y = Mathf.Clamp(position.y, _offsetScreenBorder.y, Screen.height - _offsetScreenBorder.y);
            return new Vector3(x, y, position.z);
        }
    }
}