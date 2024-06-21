using ArenaShooter.Components;
using System;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public abstract class BaseProjectileDamageMechanic : MonoBehaviour
    {
        public event Action<GameObject> HitGameObject;

        public void OnHit(Collider2D obj)
        {
            HitGameObject?.Invoke(obj.gameObject);

            OnHitMechanic(obj);
        }

        protected abstract void OnHitMechanic(Collider2D obj);
    }
}