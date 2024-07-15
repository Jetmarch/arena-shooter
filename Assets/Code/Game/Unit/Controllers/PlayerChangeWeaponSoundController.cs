using ArenaShooter.Audio;
using ArenaShooter.Weapons;
using Zenject;


namespace ArenaShooter.Units
{
    public class PlayerChangeWeaponSoundController : IInitializable, ILateDisposable
    {
        private WeaponChangeMechanic _weaponChangeMechanic;
        private AudioComponent _audioComponent;
        private string _changeWeaponSoundName;

        public PlayerChangeWeaponSoundController(WeaponChangeMechanic weaponChangeMechanic, AudioComponent audioComponent, string changeWeaponSoundName)
        {
            _weaponChangeMechanic = weaponChangeMechanic;
            _audioComponent = audioComponent;
            _changeWeaponSoundName = changeWeaponSoundName;
        }

        public void Initialize()
        {
            _weaponChangeMechanic.WeaponChanged += PlaySound;
        }
        public void LateDispose()
        {
            _weaponChangeMechanic.WeaponChanged -= PlaySound;
        }

        private void PlaySound()
        {
            _audioComponent.Play(_changeWeaponSoundName);
        }
    }
}