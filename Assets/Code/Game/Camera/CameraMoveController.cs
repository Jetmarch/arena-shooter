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
        private float _maxViewDistance = 10f;

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
            //Камера фиксируется на точке между игроком и указателем мыши
            var newTargetPosition = (_player.position + mousePos) * 0.5f;
            newTargetPosition = Vector3.ClampMagnitude(newTargetPosition, _maxViewDistance);
            Debug.Log(newTargetPosition.magnitude);

            _followTargetComponent.Follow(newTargetPosition);
        }
    }
}