using System;
using System.Collections;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileImpactAfterDelayMechanic : MonoBehaviour
    {
        [SerializeField]
        private float _delay;

        public event Action OnImpact;

        private void Start()
        {
            StartCoroutine(DelayBeforeImpact());
        }

        private IEnumerator DelayBeforeImpact()
        {
            yield return new WaitForSeconds(_delay);
            OnImpact?.Invoke();
        }
    }
}