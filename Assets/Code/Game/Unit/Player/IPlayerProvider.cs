using System;
using UnityEngine;

namespace ArenaShooter.Units.Player
{
    public interface IPlayerProvider
    {
        PlayerFacade Player { get; }
        event Action<PlayerFacade> OnPlayerCreated;
    }
}