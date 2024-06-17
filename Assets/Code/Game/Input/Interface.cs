using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Inputs
{
    public interface IShootInputProvider
    {
        event Action OnShoot;
    }

    public interface IDashInputProvider
    {
        event Action OnDash;
    }

    public interface IChangeWeaponInputProvider
    {
        event Action OnChangeWeaponUp;
        event Action OnChangeWeaponDown;
    }


    public interface IReloadInputProvider
    {
        event Action OnReload;
    }

    public interface IMoveInputProvider
    {
        event Action<Vector2> OnMove;
    }

    public interface IScreenMouseMoveInputProvider
    {
        event Action<Vector3> OnScreenMouseMove;
    }

    public interface IWorldMouseMoveInputProvider
    {
        event Action<Vector3> OnWorldMouseMove;
    }
}