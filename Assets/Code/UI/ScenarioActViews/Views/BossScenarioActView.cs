using TMPro;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class BossScenarioActView : MonoBehaviour
    {
        [SerializeField]
        private ProgressBarView _progressBarView;

        [SerializeField]
        private TextMeshProUGUI _bossNameText;

        public void Setup(string bossName, float currentProgress, float maxProgress)
        {
            _progressBarView.gameObject.SetActive(true);
            _bossNameText.gameObject.SetActive(true);
            _progressBarView.Setup(currentProgress, maxProgress);
            _bossNameText.text = bossName;
        }

        public void UpdateProgress(float progress)
        {
            _progressBarView.Show(progress);
        }

        public void Hide()
        {
            _progressBarView.gameObject.SetActive(false);
            _bossNameText.gameObject.SetActive(false);
        }
    }
}