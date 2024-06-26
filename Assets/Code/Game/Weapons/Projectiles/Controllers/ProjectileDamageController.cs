using ArenaShooter.Components.Triggers;
using Zenject;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileDamageController : IInitializable, ILateDisposable
    {
        private Trigger2DComponent _triggerComponent;
        private IProjectileDamageMechanic _damageMechanic;

        public ProjectileDamageController(Trigger2DComponent triggerComponent, IProjectileDamageMechanic damageMechanic)
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