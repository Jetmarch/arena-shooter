using TMPro;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class HordeScenarioActView : MonoBehaviour
    {
        [SerializeField]
        private ProgressBarView _progressBarView;
        [SerializeField]
        private TextMeshProUGUI _hordeLabel;

        public void Setup(float currentValue, float maxValue)
        {
            _progressBarView.gameObject.SetActive(true);
            _hordeLabel.gameObject.SetActive(true);
            _progressBarView.Setup(currentValue, maxValue);
        }

        public void UpdateProgress(float progress)
        {
            _progressBarView.Show(progress);
        }

        public void Hide()
        {
            _progressBarView.gameObject.SetActive(false);
            _hordeLabel.gameObject.SetActive(false);
        }
    }
}