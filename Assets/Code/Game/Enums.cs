using System;

namespace ArenaShooter
{
    [Serializable]
    public enum AIType
    {
        Melee,
        Ranged
    }

    [Serializable]
    public enum GameState
    {
        None,
        Running,
        Paused,
        Finished
    }
}