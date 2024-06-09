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
    public class WeaponRotateController : MonoBehaviour
    {
        private BaseInputController _inputController;
        private WeaponSetController _weaponSetController;

        [SerializeField]
        private Transform _weaponLeftSide;
        [SerializeField]
        private Transform _weaponRightSide;
        [SerializeField]
        private float _minUpperSideZRotation = 0f;
        [SerializeField]
        private float _maxUpperSideZRotation = 102f;
        [SerializeField]
        private float _minBottomSideZRotation = 240f;
        [SerializeField]
        private float _maxBottomSideZRotation = 360f;

        private Camera _camera;
        private Vector3 _lastMousePos;

        [Inject]
        private void Construct(BaseInputController inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _weaponSetController = GetComponent<WeaponSetController>();
            _camera = Camera.main;
            _weaponSetController.WeaponChanged += OnWeaponChanged;
        }

        private void OnWeaponChanged()
        {
            RotateWeapon(_lastMousePos);
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
            //TODO: Перенести в отдельный класс?
            ChangeWeaponSide();
            RotateWeapon(mousePos);
            _lastMousePos = mousePos;
        }

        private void RotateWeapon(Vector3 mousePos)
        {
            //TODO: Подумать над поворотом оружия у ботов
            var currentWeapon = _weaponSetController.CurrentWeapon;

            Vector3 mouseWorldPoint = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));

            float angleRad = Mathf.Atan2(mouseWorldPoint.y - transform.position.y, mouseWorldPoint.x - transform.position.x);
            float angle = (180 / Mathf.PI) * angleRad;

            currentWeapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private void ChangeWeaponSide()
        {
            var currentWeapon = _weaponSetController.CurrentWeapon;
            if ((currentWeapon.transform.eulerAngles.z > _minUpperSideZRotation && currentWeapon.transform.eulerAngles.z < _maxUpperSideZRotation) || (currentWeapon.transform.eulerAngles.z > _minBottomSideZRotation && currentWeapon.transform.eulerAngles.z < _maxBottomSideZRotation))
            {
                currentWeapon.transform.position = _weaponRightSide.position;
                currentWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = false;
            }
            else
            {
                currentWeapon.transform.position = _weaponLeftSide.position;
                currentWeapon.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
    }
}