using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// Поворачивает оружие персонажа в сторону указателя мыши
    /// </summary>
    public sealed class WeaponRotateObserver : MonoBehaviour
    {
        [SerializeField]
        private Transform _weaponLeftSide;
        [SerializeField]
        private Transform _weaponRightSide;
        [SerializeField]
        private float _minUpperSideZRotationRight = 0f;
        [SerializeField]
        private float _maxUpperSideZRotationRight = 102f;
        [SerializeField]
        private float _minBottomSideZRotationRight = 240f;
        [SerializeField]
        private float _maxBottomSideZRotationRight = 360f;

        [SerializeField]
        private float _minUpperSideZRotationLeft = 135f;
        [SerializeField]
        private float _maxUpperSideZRotationLeft = 225f;

        [SerializeField]
        private float _spaceBetweenSides = 50f;

        private Camera _camera;
        private bool _isWeaponOnRightSide;
        private IMouseMoveInputProvider _inputController;

        [Inject]
        private void Construct(IMouseMoveInputProvider inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _camera = Camera.main;
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
            ChangeWeaponSide();
            RotateWeapon(mousePos);
        }

        private void RotateWeapon(Vector3 mousePos)
        {
            Vector3 mouseWorldPoint = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));

            float angleRad = Mathf.Atan2(mouseWorldPoint.y - transform.position.y, mouseWorldPoint.x - transform.position.x);
            float angle = (180 / Mathf.PI) * angleRad;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void ChangeWeaponSide()
        {
            if ((transform.eulerAngles.z > _minUpperSideZRotationRight && transform.eulerAngles.z < _maxUpperSideZRotationRight) 
                || (transform.eulerAngles.z > _minBottomSideZRotationRight && transform.eulerAngles.z < _maxBottomSideZRotationRight))
            {
                if (_isWeaponOnRightSide) return;
                
                //TODO: Переместить логику в отдельный класс
                gameObject.GetComponentInChildren<SpriteRenderer>().flipY = false;
                _isWeaponOnRightSide = true;
                
            }
            else if ((transform.eulerAngles.z > _minUpperSideZRotationLeft && transform.eulerAngles.z < _maxUpperSideZRotationLeft))
            {
                if (!_isWeaponOnRightSide) return;
                
                gameObject.GetComponentInChildren<SpriteRenderer>().flipY = true;
                _isWeaponOnRightSide = false;
                
            }
        }
    }
}