using ArenaShooter.Inputs;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// Отражает спрайт оружия по оси Y
    /// </summary>
    public class WeaponFlipSpriteMechanic : MonoBehaviour
    {
        private IScreenMouseMoveInputProvider _inputController;

        private SpriteRenderer _spriteRenderer;

        public void Construct(IScreenMouseMoveInputProvider inputController)
        {
            _inputController = inputController;
            _inputController.OnScreenMouseMove += OnMouseMove;
        }

        private void Start()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void OnEnable()
        {
            if (_inputController == null) return;
            _inputController.OnScreenMouseMove += OnMouseMove;
        }

        private void OnDisable()
        {
            if (_inputController == null) return;
            _inputController.OnScreenMouseMove -= OnMouseMove;
        }

        private void OnMouseMove(Vector3 mousePos)
        {
            FlipWeaponSprite(mousePos);
        }

        private void FlipWeaponSprite(Vector3 mousePos)
        {
            if (_spriteRenderer == null) return;
            //Проверяка на какой половине экрана находится указатель мыши
            //Левая сторона
            if (mousePos.x < Screen.width * 0.5f)
            {
                _spriteRenderer.flipY = true;
            }
            //Правая сторона
            else
            {
                _spriteRenderer.flipY = false;
            }
        }
    }
}