using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// ѕоворачивает оружие персонажа в сторону указател€ мыши
    /// </summary>
    public sealed class WeaponRotateObserver : MonoBehaviour
    {
        private BaseInputController _inputController;

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

        [Inject]
        private void Construct(BaseInputController inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            _inputController.MouseMove += OnMouseMove;
        }

        private void OnDisable()
        {
            _inputController.MouseMove -= OnMouseMove;
        }

        private void OnMouseMove(Vector3 mousePos)
        {
            //TODO: ѕеренести в отдельный класс?
            ChangeWeaponSide();
            RotateWeapon(mousePos);
        }

        private void RotateWeapon(Vector3 mousePos)
        {
            //TODO: ѕодумать над поворотом оружи€ у ботов

            Vector3 mouseWorldPoint = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));

            float angleRad = Mathf.Atan2(mouseWorldPoint.y - transform.position.y, mouseWorldPoint.x - transform.position.x);
            float angle = (180 / Mathf.PI) * angleRad;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private void ChangeWeaponSide()
        {
            //TODO: ѕофиксить баг с частой сменой стороны оружи€, когда курсор мыши находитс€ пр€мо на персонаже
            if ((transform.eulerAngles.z > _minUpperSideZRotationRight && transform.eulerAngles.z < _maxUpperSideZRotationRight) 
                || (transform.eulerAngles.z > _minBottomSideZRotationRight && transform.eulerAngles.z < _maxBottomSideZRotationRight))
            {
                if (_isWeaponOnRightSide) return;
                transform.position = _weaponRightSide.position;
                gameObject.GetComponent<SpriteRenderer>().flipY = false;
                _isWeaponOnRightSide = true;
                Debug.Log("ChangeWeaponSide: Right");
            }
            else if ((transform.eulerAngles.z > _minUpperSideZRotationLeft && transform.eulerAngles.z < _maxUpperSideZRotationLeft))
            {
                if (!_isWeaponOnRightSide) return;
                transform.position = _weaponLeftSide.position;
                gameObject.GetComponent<SpriteRenderer>().flipY = true;
                _isWeaponOnRightSide = false;
                Debug.Log("ChangeWeaponSide: Left");
            }
        }
    }
}