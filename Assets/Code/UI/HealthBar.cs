using System;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private GameObject _fullHeartPrefab;

        [SerializeField]
        private GameObject _halfHeartPrefab;

        [SerializeField]
        private GameObject _heartsContainer;

        [SerializeField]
        private int _maxHearts = 10;

        public void UpdateHealth(float health)
        {
            ClearHearts();

            int countOfHearts = Mathf.Clamp((int)health, 0, _maxHearts);

            for (int i = 0; i < countOfHearts; i++)
            {
                CreateHeart(_fullHeartPrefab);
            }

            if (countOfHearts == _maxHearts) return;

            var fractional = health - Math.Truncate(health);

            if (fractional >= 0.01f)
            {
                CreateHeart(_halfHeartPrefab);
            }
        }

        private void ClearHearts()
        {
            foreach (Transform heart in _heartsContainer.transform)
            {
                Destroy(heart.gameObject);
            }
        }

        private void CreateHeart(GameObject heartPrefab)
        {
            Instantiate(heartPrefab, _heartsContainer.transform);
        }
    }
}