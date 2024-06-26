using ArenaShooter.Components;
using UnityEngine;
using Zenject;

namespace ArenaShooter.CameraControllers
{
    [RequireComponent(typeof(FollowTargetComponent))]
    public class CameraMoveMechanic : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private float _maxViewDistance = 5f;

        [SerializeField]
        private FollowTargetComponent _followTargetComponent;

        private Camera _camera;

        private Vector3 _mousePos;

        [Inject]
        public void Constuct(Camera camera)
        {
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

        public void MoveTo(Vector3 mousePos)
        {
            _mousePos = mousePos;
        }

        public void OnFixedUpdate(float delta)
        {
            if (_target == null) return;

            Follow(_mousePos);
        }

        private void Follow(Vector3 position)
        {
            var mousePos = _camera.ScreenToWorldPoint(position);
            mousePos.z = 0;
            //Камера фиксируется на точке между игроком и указателем мыши
            var newTargetPosition = (_target.position + mousePos) * 0.5f;
            //Ограничиваем максимальную точку удаления камеры от персонажа игрока. Это сделано для того, чтобы игрок всегда видел персонажа
            newTargetPosition = Vector3.ClampMagnitude(newTargetPosition - _target.position, _maxViewDistance) + _target.position;

            _followTargetComponent.Follow(newTargetPosition);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _followTargetComponent = GetComponent<FollowTargetComponent>();
        }
#endif
    }
}