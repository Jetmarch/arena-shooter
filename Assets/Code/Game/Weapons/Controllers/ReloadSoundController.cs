using ArenaShooter.Audio;
using UnityEngine;
using Zenject;


namespace ArenaShooter.Weapons
{
    public class ReloadSoundController : IInitializable, ILateDisposable
    {
        private WeaponReloadMechanic _weaponReloadMechanic;
        private AudioComponent _audioComponent;
        private string _reloadSoundName;

        public ReloadSoundController(WeaponReloadMechanic weaponReloadMechanic, AudioComponent audioComponent, string reloadSoundName)
        {
            _weaponReloadMechanic = weaponReloadMechanic;
            _audioComponent = audioComponent;
            _reloadSoundName = reloadSoundName;
        }

        public void Initialize()
        {
            _weaponReloadMechanic.OnStartReload += PlaySound;
        }

        public void LateDispose()
        {
            _weaponReloadMechanic.OnStartReload -= PlaySound;
        }

        private void PlaySound()
        {
            _audioComponent.Play(_reloadSoundName);
        }
    }
}