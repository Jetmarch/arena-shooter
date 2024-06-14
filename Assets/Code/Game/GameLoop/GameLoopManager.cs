using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter
{
    public class GameLoopManager : MonoBehaviour
    {
        [SerializeField]
        private List<IGameLoopListener> _listeners = new();
        [SerializeField]
        private List<IGameUpdateListener> _updateListeners = new();
        [SerializeField]
        private List<IGameLateUpdateListener> _lateUpdateListeners = new();
        [SerializeField]
        private List<IGameFixedUpdateListener> _fixedUpdateListeners = new();

        public void AddListener(IGameLoopListener listener)
        {
            _listeners.Add(listener);

            if (listener is IGameUpdateListener updateListener)
            {
                _updateListeners.Add(updateListener);
            }
            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Add(lateUpdateListener);
            }
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Add(fixedUpdateListener);
            }
        }

        public void RemoveListener(IGameLoopListener listener)
        {
            _listeners.Remove(listener);

            if(listener is IGameUpdateListener updateListener)
            {
                _updateListeners.Remove(updateListener);
            }
            if(listener is IGameLateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Remove(lateUpdateListener);
            }
            if(listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Remove(fixedUpdateListener);
            }
        }

        public void StartGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameStartListener startGameListener)
                {
                    startGameListener.OnStartGame();
                }
            }
        }

        public void PauseGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGamePauseListener pauseGameListener)
                {
                    pauseGameListener.OnPauseGame();
                }
            }
        }

        public void ResumeGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameResumeListener resumeGameListener)
                {
                    resumeGameListener.OnResumeGame();
                }
            }
        }

        public void FinishGame()
        {
            foreach (var listener in _listeners)
            {
                if (listener is IGameFinishListener finishGameListener)
                {
                    finishGameListener.OnFinishGame();
                }
            }
        }

        private void Update()
        {
            var delta = Time.deltaTime;

            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(delta);
            }
        }

        private void LateUpdate()
        {
            var delta = Time.deltaTime;

            for (int i = 0; i < _lateUpdateListeners.Count; i++)
            {
                _lateUpdateListeners[i].OnLateUpdate(delta);
            }
        }

        private void FixedUpdate()
        {
            var delta = Time.deltaTime;

            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(delta);
            }
        }
    }
}