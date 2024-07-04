using ArenaShooter.Inputs;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class WeaponSpriteFlipController : IInitializable, ILateDisposable
    {
        private WeaponFlipSpriteMechanic _flipMechanic;
        private IScreenMouseMoveInputProvider _screenMouseMoveInputProvider;

        public WeaponSpriteFlipController(WeaponFlipSpriteMechanic flipMechanic, IScreenMouseMoveInputProvider screenMouseMoveInputProvider)
        {
            _flipMechanic = flipMechanic;
            _screenMouseMoveInputProvider = screenMouseMoveInputProvider;
        }

        public void Initialize()
        {
            _screenMouseMoveInputProvider.OnScreenMouseMove += _flipMechanic.FlipWeaponSprite;
        }

        public void LateDispose()
        {
            _screenMouseMoveInputProvider.OnScreenMouseMove -= _flipMechanic.FlipWeaponSprite;
        }
    }
}