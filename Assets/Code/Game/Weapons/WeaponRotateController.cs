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

        [Inject]
        private void Construct(BaseInputController inputController)
        {
            _inputController = inputController;
        }

        private void Start()
        {
            _weaponSetController = GetComponent<WeaponSetController>();
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
            RotateWeapon(mousePos);

            //TODO: Перенести в отдельный класс?
            ChangeWeaponSide();
        }

        private void RotateWeapon(Vector3 mousePos)
        {
            var newPos = _camera.ScreenToWorldPoint(mousePos);
            newPos = new Vector3(newPos.x, newPos.y, 0f);
            var currentWeapon = _weaponSetController.CurrentWeapon;
            var newRot = Quaternion.LookRotation(newPos, currentWeapon.transform.TransformDirection(Vector3.up));
            currentWeapon.transform.rotation = new Quaternion(0f, 0f, newRot.z, newRot.w);
        }

        private void ChangeWeaponSide()
        {
            var currentWeapon = _weaponSetController.CurrentWeapon;
            if ((currentWeapon.transform.eulerAngles.z > _minUpperSideZRotation && currentWeapon.transform.eulerAngles.z < _maxUpperSideZRotation) || (currentWeapon.transform.eulerAngles.z > _minBottomSideZRotation && currentWeapon.transform.eulerAngles.z < _maxBottomSideZRotation))
            {
                currentWeapon.transform.position = _weaponRightSide.position;
            }
            else
            {
                currentWeapon.transform.position = _weaponLeftSide.position;
            }
        }
    }
}