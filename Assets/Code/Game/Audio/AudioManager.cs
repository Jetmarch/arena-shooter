using RotaryHeart.Lib.SerializableDictionary;
using System;
using UnityEngine;

namespace ArenaShooter.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClipStorage _audioClipStorage;

        [SerializeField]
        private AudioSource _ambienceAudioSource;
        [SerializeField]
        private AudioSource _soundsAudioSource;
        [SerializeField]
        private AudioSource _musicAudioSource;

        public AudioClip GetSound(string name)
        {
            return _audioClipStorage.GetSound(name);
        }

        public void PlayAmbience(string name)
        {
            _ambienceAudioSource.clip = GetSound(name);
            _ambienceAudioSource.Play();
        }

        public void PlaySound(string name)
        {
            _soundsAudioSource.clip = GetSound(name);
            _soundsAudioSource.Play();
        }

        public void PlayMusic(string name)
        {
            _musicAudioSource.clip = GetSound(name);
            _musicAudioSource.Play();
        }
    }
}
