using ArenaShooter.Inputs;
using System.Collections;
using System.Collections.Generic;
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

        
        private IShootInputProvider _inputProvider;
        public void Construct(IShootInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _canShoot = true;
            _inputProvider.OnShoot += OnShoot;
        }

        private void OnShoot()
        {
            if (!_canShoot) return;

            StartCoroutine(DelayBetweetShots());
        }

        private void OnEnable()
        {
            if (_inputProvider == null) return;
            _inputProvider.OnShoot += OnShoot;
        }

        private void OnDisable()
        {
            if (_inputProvider == null) return;
            _inputProvider.OnShoot -= OnShoot;
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