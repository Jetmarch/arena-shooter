using UnityEngine;
using Zenject;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// Отражает спрайт оружия по оси Y
    /// </summary>
    public class WeaponFlipSpriteMechanic : IInitializable, ILateDisposable, IGamePauseListener
    {
        private SpriteRenderer _spriteRenderer;
        private bool _isPaused;

        public WeaponFlipSpriteMechanic(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
        }

        public void FlipWeaponSprite(Vector3 mousePos)
        {
            if (_isPaused) return;

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

        public void Initialize()
        {
            IGameLoopListener.Register(this);
        }

        public void LateDispose()
        {
            IGameLoopListener.Unregister(this);
        }

        public void OnPauseGame()
        {
            _isPaused = true;
        }

        public void OnResumeGame()
        {
            _isPaused = false;
        }
    }
}