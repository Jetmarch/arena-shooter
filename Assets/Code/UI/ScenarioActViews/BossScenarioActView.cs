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
            _progressBarView.Setup(currentProgress, maxProgress);
            _bossNameText.text = bossName;
        }

        public void Show(float currentProgress)
        {
            _progressBarView.Show(currentProgress);
        }
    }
}