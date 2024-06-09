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
        private BaseInputController _inputController;

        [Inject]
        public void Constuct(BaseInputController inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _camera = Camera.main;
            _followTargetComponent = GetComponent<FollowTargetComponent>();
        }

        private void FixedUpdate()
        {
            var mousePos = _camera.ScreenToWorldPoint(_inputController.GetMousePos());
            mousePos.z = 0;
            //Камера фиксируется на точке между игроком и указателем мыши
            var newTargetPosition = (_player.position + mousePos) * 0.5f;
            //Ограничиваем максимальную точку удаления камеры от персонажа игрока. Это сделано для того, чтобы игрок всегда видел персонажа
            newTargetPosition = Vector3.ClampMagnitude(newTargetPosition - _player.position, _maxViewDistance) + _player.position;

            _followTargetComponent.Follow(newTargetPosition);
        }

        //private void OnDrawGizmos()
        //{
        //    var mousePos = _camera.ScreenToWorldPoint(_inputController.GetMousePos());
        //    //Камера фиксируется на точке между игроком и указателем мыши
        //    var newTargetPosition = (_player.position + mousePos) * 0.5f;
        //    //newTargetPosition = Vector3.ClampMagnitude(newTargetPosition + _player.position, _maxViewDistance);

        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawLine(Vector3.zero, mousePos);
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawLine(Vector3.zero, _player.position);
        //    Gizmos.color = Color.green;
        //    Gizmos.DrawLine(Vector3.zero, newTargetPosition);

        //    Gizmos.color = Color.cyan;
        //    Gizmos.DrawLine(newTargetPosition, _player.position );

        //    Gizmos.color = Color.cyan;
        //    Gizmos.DrawLine(Vector3.zero, _player.position - newTargetPosition);
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawLine(Vector3.zero, Vector3.ClampMagnitude(newTargetPosition - _player.position, _maxViewDistance) + _player.position);

        //}
    }
}