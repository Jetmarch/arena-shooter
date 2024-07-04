using ArenaShooter.Audio;
using Zenject;

namespace ArenaShooter.Weapons
{
    public class ShootSoundController : IInitializable, ILateDisposable
    {
        private AudioComponent _audioComponent;
        private BaseWeaponShootMechanic _shootMechanic;
        private string _shootSoundName;

        public ShootSoundController(AudioComponent audioComponent, BaseWeaponShootMechanic shootMechanic, string shootSoundName)
        {
            _audioComponent = audioComponent;
            _shootMechanic = shootMechanic;
            _shootSoundName = shootSoundName;
        }

        public void Initialize()
        {
            _shootMechanic.ShootComplete += OnShootComplete;
        }

        public void LateDispose()
        {
            _shootMechanic.ShootComplete -= OnShootComplete;
        }

        private void OnShootComplete()
        {
            _audioComponent.Play(_shootSoundName);
        }
    }
}