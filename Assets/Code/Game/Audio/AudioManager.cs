using RotaryHeart.Lib.SerializableDictionary;
using System;
using UnityEngine;

namespace ArenaShooter.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private SerializableDictionaryBase<string, AudioClip> _audioClips;

        public AudioClip GetSound(string name)
        {
            if (_audioClips.TryGetValue(name.ToLower(), out var audioClip))
            {
                return audioClip;
            }
            else
            {
                throw new Exception($"AudioClip with key {name} not found!");
            }
        }
    }
}
