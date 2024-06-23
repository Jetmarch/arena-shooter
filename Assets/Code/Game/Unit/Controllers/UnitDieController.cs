using ArenaShooter.Components;
using Zenject;

namespace ArenaShooter.Units.Player
{
    public class UnitDieController : IInitializable, ILateDisposable
    {
        private UnitDieMechanic _unitDieMechanic;
        private HealthComponent _healthComponent;

        public UnitDieController(UnitDieMechanic unitDieMechanic, HealthComponent healthComponent)
        {
            _unitDieMechanic = unitDieMechanic;
            _healthComponent = healthComponent;
        }

        public void Initialize()
        {
            _healthComponent.CurrentHealthChanged += _unitDieMechanic.OnCurrentHealthChanged;
        }

        public void LateDispose()
        {
            _healthComponent.CurrentHealthChanged -= _unitDieMechanic.OnCurrentHealthChanged;
        }
    }
}