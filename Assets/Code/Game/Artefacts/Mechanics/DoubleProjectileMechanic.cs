using ArenaShooter.Units.Player;
using System.Collections;
using UnityEngine;

namespace ArenaShooter.Artefacts
{
    public class DoubleProjectileMechanic
    {
        private PlayerFacade _playerFacade;
        private float _doubleShotChance = 1f;
        private float _delayBetweenShots = 0.3f;
        private MonoBehaviour _coroutineCreator;

        public DoubleProjectileMechanic(IPlayerProvider playerProvider, float doubleShotChance, float delayBetweenShots, MonoBehaviour coroutineCreator)
        {
            _playerFacade = playerProvider.Player;
            _doubleShotChance = doubleShotChance;
            _delayBetweenShots = delayBetweenShots;
            _coroutineCreator = coroutineCreator;
        }

        public void DoubleShot()
        {
            if (Random.value > _doubleShotChance) return;



            _coroutineCreator.StartCoroutine(DelayedShot());
        }

        private IEnumerator DelayedShot()
        {
            yield return new WaitForSeconds(_delayBetweenShots);
            _playerFacade.CurrentWeapon.WeaponShootMechanic.ShootMechanic();
        }
    }
}