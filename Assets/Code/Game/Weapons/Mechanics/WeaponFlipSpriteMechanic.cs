using UnityEngine;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// �������� ������ ������ �� ��� Y
    /// </summary>
    public class WeaponFlipSpriteMechanic
    {
        private SpriteRenderer _spriteRenderer;

        public WeaponFlipSpriteMechanic(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
        }

        public void FlipWeaponSprite(Vector3 mousePos)
        {
            if (_spriteRenderer == null) return;
            //��������� �� ����� �������� ������ ��������� ��������� ����
            //����� �������
            if (mousePos.x < Screen.width * 0.5f)
            {
                _spriteRenderer.flipY = true;
            }
            //������ �������
            else
            {
                _spriteRenderer.flipY = false;
            }
        }
    }
}