using RotaryHeart.Lib.SerializableDictionary;
using System;
using UnityEditor;
using UnityEngine;


namespace ArenaShooter.Audio
{
    [CreateAssetMenu(fileName = "AudioClipStorage", menuName = "SO/Configs/AudioClipStorage")]
    public class AudioClipStorage : ScriptableObject
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

#if UNITY_EDITOR
        [ContextMenu("Find all audio clips")]
        private void FindAllAudioClips()
        {
            _audioClips = new SerializableDictionaryBase<string, AudioClip> ();
            var audioClipGuids = AssetDatabase.FindAssets("t:audioClip", new[] {"Assets/Audio/Sound", "Assets/Audio/Music", "Assets/Audio/Sound/ShootingSound" });
            foreach(var guid in audioClipGuids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var audioClip = (AudioClip)AssetDatabase.LoadAssetAtPath(assetPath, typeof(AudioClip));
                _audioClips.Add(audioClip.name, audioClip);
            }
        }
#endif
    }
}