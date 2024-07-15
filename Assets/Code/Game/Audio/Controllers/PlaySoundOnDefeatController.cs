using UnityEngine;
using Zenject;

namespace ArenaShooter.Audio
{
    public class PlaySoundOnDefeatController : IInitializable, ILateDisposable
    {
        private GameConditionManager _gameConditionManager;
        private AudioManager _audioManager;
        private string _defeatSoundName;

        public PlaySoundOnDefeatController(GameConditionManager gameConditionManager, AudioManager audioManager, string defeatSoundName)
        {
            _gameConditionManager = gameConditionManager;
            _audioManager = audioManager;
            _defeatSoundName = defeatSoundName;
        }

        public void Initialize()
        {
            _gameConditionManager.OnDefeat += PlaySound;
        }

        public void LateDispose()
        {
            _gameConditionManager.OnDefeat -= PlaySound;
        }

        private void PlaySound()
        {
            _audioManager.PlaySound(_defeatSoundName);
        }
    }
}