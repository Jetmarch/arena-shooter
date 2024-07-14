using ArenaShooter.Components.Triggers;
using Zenject;

namespace ArenaShooter.Mechanics
{
    public class ImpactController : IInitializable, ILateDisposable
    {
        private Trigger2DComponent _triggerComponent;
        private IImpactMechanic _impactMechanic;

        public ImpactController(Trigger2DComponent triggerComponent, IImpactMechanic impactMechanic)
        {
            _triggerComponent = triggerComponent;
            _impactMechanic = impactMechanic;
        }

        public void Initialize()
        {
            _triggerComponent.TriggerOn += _impactMechanic.OnHit;
        }

        public void LateDispose()
        {
            _triggerComponent.TriggerOn -= _impactMechanic.OnHit;
        }
    }
}