using System;
using UnityEngine;

namespace ArenaShooter.Mechanics
{
    public interface IDamageMechanic
    {
        event Action<GameObject> HitGameObject;
        GameObject Owner { get; set; }
        void OnHit(GameObject obj);
    }
}