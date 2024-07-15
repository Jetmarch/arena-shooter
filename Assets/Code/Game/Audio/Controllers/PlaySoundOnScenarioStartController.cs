using ArenaShooter.Scenarios;
using UnityEngine;
using Zenject;


namespace ArenaShooter.Audio
{
    public class PlaySoundOnScenarioStartController : IInitializable, ILateDisposable
    {
        private ArenaScenarioExecutor _scenarioExecutor;
        private AudioManager _audioManager;
        private string _scenarioStartSoundName;

        public PlaySoundOnScenarioStartController(ArenaScenarioExecutor scenarioExecutor, AudioManager audioManager, string scenarioStartSoundName)
        {
            _scenarioExecutor = scenarioExecutor;
            _audioManager = audioManager;
            _scenarioStartSoundName = scenarioStartSoundName;
        }

        public void Initialize()
        {
            _scenarioExecutor.OnScenarioStart += PlaySound;
        }

        public void LateDispose()
        {
            _scenarioExecutor.OnScenarioStart -= PlaySound;
        }

        private void PlaySound()
        {
            _audioManager.PlaySound(_scenarioStartSoundName);
        }
    }
}