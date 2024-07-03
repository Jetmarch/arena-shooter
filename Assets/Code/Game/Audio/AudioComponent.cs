using UnityEngine;


namespace ArenaShooter.Audio
{
    public class AudioComponent
    {
        private AudioManager _audioManager;
        private AudioSource _audioSource;

        public AudioComponent(AudioManager audioManager, AudioSource audioSource)
        {
            _audioManager = audioManager;
            _audioSource = audioSource;
        }

        public void Play(string soundName)
        {
            _audioSource.clip = _audioManager.GetSound(soundName);
            _audioSource.Play();
        }
    }
}
