using ArenaShooter.Inputs;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// Отражает спрайт оружия по оси Y
    /// </summary>
    public class WeaponFlipSpriteMechanic : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public void Construct(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
        }

        public void FlipWeaponSprite(Vector3 mousePos)
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