using ArenaShooter.Components;
using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ArenaShooter.CameraControllers
{
    [RequireComponent(typeof(FollowTargetComponent))]
    public class CameraMoveController : MonoBehaviour
    {
        [SerializeField]
        private Transform _player;

        [SerializeField]
        private float _maxViewDistance = 5f;

        private FollowTargetComponent _followTargetComponent;
        private Camera _camera;
        private IMouseMoveInputProvider _inputController;

        private Vector3 _mousePos;

        [Inject]
        public void Constuct(IMouseMoveInputProvider inputController)
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
            _inputController.OnMouseMove += OnMouseMove;
        }

        private void OnDisable()
        {
            _inputController.OnMouseMove -= OnMouseMove;
        }

        private void OnMouseMove(Vector3 mousePos)
        {
            _mousePos = mousePos;
        }

        private void FixedUpdate()
        {
            Follow(_mousePos);
        }

        private void Follow(Vector3 position)
        {
            //TODO: Добавить контроллер
            var mousePos = _camera.ScreenToWorldPoint(position);
            mousePos.z = 0;
            //Камера фиксируется на точке между игроком и указателем мыши
            var newTargetPosition = (_player.position + mousePos) * 0.5f;
            //Ограничиваем максимальную точку удаления камеры от персонажа игрока. Это сделано для того, чтобы игрок всегда видел персонажа
            newTargetPosition = Vector3.ClampMagnitude(newTargetPosition - _player.position, _maxViewDistance) + _player.position;

            _followTargetComponent.Follow(newTargetPosition);
        }
    }
}