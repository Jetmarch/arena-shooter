using ArenaShooter.Inputs;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// Поворачивает оружие персонажа в сторону указателя мыши
    /// TODO: Нужна обертка, которая будет переводить позицию мышки в позицию мировых координат
    /// И вызывать этот компонент. Это требуется для корректного использования компонента как игроком
    /// Так и ботами
    /// </summary>
    public sealed class WeaponRotateMechanic : MonoBehaviour
    {
        private Camera _camera;

        private IWorldMouseMoveInputProvider _inputController;

        public void Construct(IWorldMouseMoveInputProvider inputController)
        {
            _inputController = inputController;
            _inputController.OnWorldMouseMove += OnMouseMove;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnEnable()
        {
            if (_inputController == null) return;
            _inputController.OnWorldMouseMove += OnMouseMove;
        }

        private void OnDisable()
        {
            if (_inputController == null) return;
            _inputController.OnWorldMouseMove -= OnMouseMove;
        }

        private void OnMouseMove(Vector3 mousePos)
        {

            RotateWeapon(mousePos);
        }

        private void RotateWeapon(Vector3 mousePos)
        {
            if (_camera == null) return;

            //Vector3 mouseWorldPoint = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));

            float angleRad = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
            float angle = (180 / Mathf.PI) * angleRad;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
}