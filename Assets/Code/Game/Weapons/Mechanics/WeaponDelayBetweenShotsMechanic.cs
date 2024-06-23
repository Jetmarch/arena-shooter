using ArenaShooter.Inputs;
using System.Collections;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// TODO: Вынести отдельный компонент - ActionDelayComponent
    /// </summary>
    public class WeaponDelayBetweenShotsMechanic : MonoBehaviour
    {
        [SerializeField]
        private float _delayBetweenShots = 0.1f;
        [SerializeField]
        private bool _canShoot;

        
        public void Construct()
        {
            _canShoot = true;
        }

        public void OnShoot()
        {
            if (!_canShoot) return;
            StartCoroutine(DelayBetweetShots());
        }


        private IEnumerator DelayBetweetShots()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_delayBetweenShots);
            _canShoot = true;
        }

        public bool CanShoot()
        {
            return _canShoot;
        }
    }
}