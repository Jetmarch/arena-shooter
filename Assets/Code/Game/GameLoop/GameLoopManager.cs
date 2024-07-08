using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter
{
    [DefaultExecutionOrder(-10000)]
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

        private GameState _state;

        public GameState State { get { return _state; } }

        private void Awake()
        {
            IGameLoopListener.OnRegister += OnRegister;
            IGameLoopListener.OnUnregister += OnUnregister;
        }

        private void OnUnregister(IGameLoopListener obj)
        {
            RemoveListener(obj);
        }

        private void OnRegister(IGameLoopListener obj)
        {
            AddListener(obj);
        }

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

            if (listener is IGameUpdateListener updateListener)
            {
                _updateListeners.Remove(updateListener);
            }
            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Remove(lateUpdateListener);
            }
            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Remove(fixedUpdateListener);
            }
        }

        [ContextMenu("Start game")]
        public void StartGame()
        {
            _state = GameState.Running;
            for (int i = 0; i < _listeners.Count; i++)
            {
                if (_listeners[i] is IGameStartListener startGameListener)
                {
                    startGameListener.OnStartGame();
                }
            }
        }
        [ContextMenu("Pause game")]
        public void PauseGame()
        {
            _state = GameState.Paused;
            for (int i = 0; i < _listeners.Count; i++)
            {
                if (_listeners[i] is IGamePauseListener pauseGameListener)
                {
                    pauseGameListener.OnPauseGame();
                }
            }
        }
        [ContextMenu("Resume game")]
        public void ResumeGame()
        {
            _state = GameState.Running;
            for (int i = 0; i < _listeners.Count; i++)
            {
                if (_listeners[i] is IGameResumeListener resumeGameListener)
                {
                    resumeGameListener.OnResumeGame();
                }
            }
        }
        [ContextMenu("Finish game")]
        public void FinishGame()
        {
            _state = GameState.Finished;
            for (int i = 0; i < _listeners.Count; i++)
            {
                if (_listeners[i] is IGameFinishListener finishGameListener)
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
            var delta = Time.fixedDeltaTime;

            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(delta);
            }
        }
    }
}