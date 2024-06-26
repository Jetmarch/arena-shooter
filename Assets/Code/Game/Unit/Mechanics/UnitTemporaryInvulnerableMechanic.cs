using System.Collections;
using UnityEngine;

namespace ArenaShooter.Units
{
    public class UnitTemporaryInvulnerableMechanic : MonoBehaviour
    {
        [SerializeField]
        private bool _isInvulnerable = false;

        [SerializeField]
        private float _invulnerableTime;

        public void SetInvulnerable()
        {
            StartCoroutine(TemporaryInvulnerability());
        }

        public bool IsNotInvulnerable()
        {
            return !_isInvulnerable;
        }

        private IEnumerator TemporaryInvulnerability()
        {
            _isInvulnerable = true;
            yield return new WaitForSeconds(_invulnerableTime);
            _isInvulnerable = false;
        }
    }
}