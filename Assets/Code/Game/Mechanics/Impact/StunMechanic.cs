using ArenaShooter.AI;
using System;
using UnityEngine;

namespace ArenaShooter.Mechanics
{
    public class StunMechanic : MonoBehaviour, IImpactMechanic
    {
        private GameObject _owner;
        public GameObject Owner { get => _owner; set => _owner = value; }

        public event Action<GameObject> HitGameObject;

        public void OnHit(GameObject obj)
        {
            if (_owner == obj) return;

            var aiBrain = obj.GetComponent<IAIBrain>();
            if(aiBrain == null) return;

            aiBrain.Stun();

            HitGameObject?.Invoke(obj);
        }
    }
}