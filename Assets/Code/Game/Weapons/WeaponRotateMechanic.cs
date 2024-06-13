using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// ������������ ������ ��������� � ������� ��������� ����
    /// </summary>
    public sealed class WeaponRotateMechanic : MonoBehaviour
    {
        private Camera _camera;
       
        private IMouseMoveInputProvider _inputController;
        
        public void Construct(IMouseMoveInputProvider inputController)
        {
            _inputController = inputController;
            _inputController.OnMouseMove += OnMouseMove;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            if (_inputController == null) return;
            _inputController.OnMouseMove += OnMouseMove;
        }

        private void OnDisable()
        {
            if (_inputController == null) return;
            _inputController.OnMouseMove -= OnMouseMove;
        }

        private void OnMouseMove(Vector3 mousePos)
        {
            
            RotateWeapon(mousePos);
        }

        private void RotateWeapon(Vector3 mousePos)
        {
            if (_camera == null) return;

            Vector3 mouseWorldPoint = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));

            float angleRad = Mathf.Atan2(mouseWorldPoint.y - transform.position.y, mouseWorldPoint.x - transform.position.x);
            float angle = (180 / Mathf.PI) * angleRad;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}