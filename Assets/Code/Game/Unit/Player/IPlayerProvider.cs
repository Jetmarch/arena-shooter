using System;

namespace ArenaShooter.Units.Player
{
    public interface IPlayerProvider
    {
        PlayerFacade Player { get; }
        event Action<PlayerFacade> OnPlayerCreated;
        event Action<PlayerFacade> OnPlayerDied;
    }
}