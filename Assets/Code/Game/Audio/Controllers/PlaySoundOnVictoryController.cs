using UnityEngine;
using Zenject;

namespace ArenaShooter.Audio
{
    public class PlaySoundOnVictoryController : IInitializable, ILateDisposable
    {
        private GameConditionManager _gameConditionManager;
        private AudioManager _audioManager;
        private string _victorySoundName;

        public PlaySoundOnVictoryController(GameConditionManager gameConditionManager, AudioManager audioManager, string victorySoundName)
        {
            _gameConditionManager = gameConditionManager;
            _audioManager = audioManager;
            _victorySoundName = victorySoundName;
        }

        public void Initialize()
        {
            _gameConditionManager.OnVictory += PlaySound;
        }

        public void LateDispose()
        {
            _gameConditionManager.OnVictory -= PlaySound;
        }

        private void PlaySound()
        {
            _audioManager.PlaySound(_victorySoundName);
        }
    }
}