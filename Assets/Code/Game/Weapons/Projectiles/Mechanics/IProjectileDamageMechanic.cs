using System;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    //public abstract class BaseProjectileDamageMechanic : MonoBehaviour
    //{
    //    public event Action<GameObject> HitGameObject;

    //    public void OnHit(Collider2D obj)
    //    {
    //        HitGameObject?.Invoke(obj.gameObject);

    //        OnHitMechanic(obj);
    //    }

    //    protected abstract void OnHitMechanic(Collider2D obj);
    //}

    public interface IProjectileDamageMechanic
    {
        event Action<GameObject> HitGameObject;
        GameObject Owner { get; set; }
        void OnHit(Collider2D obj);
    }
}