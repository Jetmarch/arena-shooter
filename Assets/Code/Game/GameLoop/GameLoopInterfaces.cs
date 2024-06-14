namespace ArenaShooter
{
    public interface IGameLoopListener
    {
        
    }

    public interface IGameStartListener : IGameLoopListener
    {
        void OnStartGame();
    }

    public interface IGamePauseListener : IGameLoopListener
    {
        void OnPauseGame();
    }

    public interface IGameResumeListener : IGameLoopListener
    {
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