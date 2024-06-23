using ArenaShooter.Inputs;
using Zenject;

namespace ArenaShooter.Units.Player
{
    public class UnitDashController : IInitializable, ILateDisposable
    {
        private UnitDashMechanic _dashMechanic;
        private IDashInputProvider _changeWeaponInputProvider;

        public UnitDashController(UnitDashMechanic dashMechanic, IDashInputProvider changeWeaponInputProvider)
        {
            _dashMechanic = dashMechanic;
            _changeWeaponInputProvider = changeWeaponInputProvider;
        }

        public void Initialize()
        {
            _changeWeaponInputProvider.OnDash += _dashMechanic.OnDash;
        }

        public void LateDispose()
        {
            _changeWeaponInputProvider.OnDash -= _dashMechanic.OnDash;
        }
    }
}