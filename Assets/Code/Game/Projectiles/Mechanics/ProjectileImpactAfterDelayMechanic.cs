using System;
using System.Collections;
using UnityEngine;

namespace ArenaShooter.Projectiles
{
    public class ProjectileImpactAfterDelayMechanic : MonoBehaviour, IGamePauseListener
    {
        [SerializeField]
        private float _delay;

        public event Action OnImpact;

        private bool _isPaused;

        private void Start()
        {
            StartCoroutine(DelayBeforeImpact());
        }

        private IEnumerator DelayBeforeImpact()
        {
            yield return new WaitForSeconds(_delay);
            yield return new WaitUntil(() => _isPaused == false);
            OnImpact?.Invoke();
        }

        public void OnPauseGame()
        {
            _isPaused = true;
        }

        public void OnResumeGame()
        {
            _isPaused = false;
        }

        private void OnEnable()
        {
            IGameLoopListener.Register(this);
        }

        private void OnDisable()
        {
            IGameLoopListener.Unregister(this);
        }
    }
}