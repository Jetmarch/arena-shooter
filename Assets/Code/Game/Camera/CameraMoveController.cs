using ArenaShooter.Components;
using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.CameraControllers
{
    [RequireComponent(typeof(FollowTargetComponent))]
    public class CameraMoveController : MonoBehaviour, IGameFixedUpdateListener
    {
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private float _maxViewDistance = 5f;

        private FollowTargetComponent _followTargetComponent;
        private Camera _camera;
        private IScreenMouseMoveInputProvider _inputController;

        private Vector3 _mousePos;

        [Inject]
        public void Constuct(IScreenMouseMoveInputProvider inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _camera = Camera.main;
            _followTargetComponent = GetComponent<FollowTargetComponent>();
        }

        private void OnEnable()
        {
            _inputController.OnScreenMouseMove += OnMouseMove;
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            _inputController.OnScreenMouseMove -= OnMouseMove;
            IGameLoopListener.Unregister(this);
        }

        private void OnMouseMove(Vector3 mousePos)
        {
            _mousePos = mousePos;
        }

        public void OnFixedUpdate(float delta)
        {
            Follow(_mousePos);
        }

        private void Follow(Vector3 position)
        {
            var mousePos = _camera.ScreenToWorldPoint(position);
            mousePos.z = 0;
            //������ ����������� �� ����� ����� ������� � ���������� ����
            var newTargetPosition = (_target.position + mousePos) * 0.5f;
            //������������ ������������ ����� �������� ������ �� ��������� ������. ��� ������� ��� ����, ����� ����� ������ ����� ���������
            newTargetPosition = Vector3.ClampMagnitude(newTargetPosition - _target.position, _maxViewDistance) + _target.position;

            _followTargetComponent.Follow(newTargetPosition);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}