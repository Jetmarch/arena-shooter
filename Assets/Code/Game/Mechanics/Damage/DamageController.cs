using ArenaShooter.Components.Triggers;
using Zenject;

namespace ArenaShooter.Mechanics
{
    public class DamageController : IInitializable, ILateDisposable
    {
        private Trigger2DComponent _triggerComponent;
        private IDamageMechanic _damageMechanic;

        public DamageController(Trigger2DComponent triggerComponent, IDamageMechanic damageMechanic)
        {
            _triggerComponent = triggerComponent;
            _damageMechanic = damageMechanic;
        }

        public void Initialize()
        {
            _triggerComponent.TriggerOn += _damageMechanic.OnHit;
        }

        public void LateDispose()
        {
            _triggerComponent.TriggerOn -= _damageMechanic.OnHit;
        }
    }
}