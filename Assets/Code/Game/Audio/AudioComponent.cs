using UnityEngine;


namespace ArenaShooter.Audio
{
    public class AudioComponent
    {
        private AudioManager _audioManager;
        private AudioSource _audioSource;

        private static float _minRandomPitch = 0.8f;
        private static float _maxRandomPitch = 1.2f;

        public AudioComponent(AudioManager audioManager, AudioSource audioSource)
        {
            _audioManager = audioManager;
            _audioSource = audioSource;
        }

        public void Play(string soundName, bool randomizePitch = true)
        {
            if(randomizePitch)
            {
                SetRandomPitch();
            }
            _audioSource.clip = _audioManager.GetSound(soundName);
            _audioSource.Play();
        }

        private void SetRandomPitch()
        {
            _audioSource.pitch = Random.Range(_minRandomPitch, _maxRandomPitch);
        }
    }
}
