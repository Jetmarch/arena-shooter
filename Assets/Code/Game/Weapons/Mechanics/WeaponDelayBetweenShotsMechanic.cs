using System.Collections;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// TODO: ������� ��������� ��������� - ActionDelayComponent
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

        //������� ���� ���� �� ������ ������ �� ����� ������� �������� ����� ����������
        private void OnEnable()
        {
            _canShoot = true;
        }

        public void DelayShot()
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