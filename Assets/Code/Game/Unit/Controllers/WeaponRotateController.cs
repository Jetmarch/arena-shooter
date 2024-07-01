using ArenaShooter.Inputs;
using ArenaShooter.Weapons;
using Zenject;

namespace ArenaShooter.Units.Player
{
    public class WeaponRotateController : IInitializable, ILateDisposable
    {
        private IWorldMouseMoveInputProvider _mouseMoveInputProvider;
        private WeaponRotateMechanic _weaponRotateMechanic;

        public WeaponRotateController(IWorldMouseMoveInputProvider mouseMoveInputProvider, WeaponRotateMechanic weaponRotateMechanic)
        {
            _mouseMoveInputProvider = mouseMoveInputProvider;
            _weaponRotateMechanic = weaponRotateMechanic;
        }

        public void Initialize()
        {
            _mouseMoveInputProvider.OnWorldMouseMove += _weaponRotateMechanic.RotateWeapon;
        }

        public void LateDispose()
        {
            _mouseMoveInputProvider.OnWorldMouseMove -= _weaponRotateMechanic.RotateWeapon;
        }
    }

}