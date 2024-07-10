using System;

namespace ArenaShooter
{
    public interface IGameLoopListener
    {
        public static event Action<IGameLoopListener> OnRegister;
        public static event Action<IGameLoopListener> OnUnregister;

        public static void Register(IGameLoopListener listener)
        {
            OnRegister?.Invoke(listener);
        }

        public static void Unregister(IGameLoopListener listener)
        {
            OnUnregister?.Invoke(listener);
        }
    }

    public interface IGameStartListener : IGameLoopListener
    {
        void OnStartGame();
    }

    public interface IGamePauseListener : IGameLoopListener
    {
        void OnPauseGame();
        void OnResumeGame();
    }

    public interface IGameFinishListener : IGameLoopListener
    {
        void OnFinishGame();
    }

    public interface IGameUpdateListener : IGameLoopListener
    {
        void OnUpdate(float delta);
    }

    public interface IGameFixedUpdateListener : IGameLoopListener
    {
        void OnFixedUpdate(float delta);
    }

    public interface IGameLateUpdateListener : IGameLoopListener
    {
        void OnLateUpdate(float delta);
    }
}