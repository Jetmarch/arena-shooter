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
            Debug.Log("Start delay");
            if (!_canShoot) return;
            Debug.Log("Delaying");
            StartCoroutine(DelayBetweetShots());
        }


        private IEnumerator DelayBetweetShots()
        {
            _canShoot = false;
            Debug.Log("CanShoot: false");
            yield return new WaitForSeconds(_delayBetweenShots);
            _canShoot = true;
            Debug.Log("CanShoot: true");
        }

        public bool CanShoot()
        {
            return _canShoot;
        }
    }
}