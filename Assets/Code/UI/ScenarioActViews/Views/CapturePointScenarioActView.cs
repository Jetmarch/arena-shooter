using TMPro;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class CapturePointScenarioActView : MonoBehaviour
    {
        [SerializeField]
        private ProgressBarView _progressBarView;

        [SerializeField]
        private UITargetPointerView _targetPointer;

        [SerializeField]
        private TextMeshProUGUI _capturePointLabel;

        public void Setup(float currentProgress, float maxProgress, Transform target, Camera camera)
        {
            _progressBarView.gameObject.SetActive(true);
            _targetPointer.gameObject.SetActive(true);
            _capturePointLabel.gameObject.SetActive(true);
            _progressBarView.Setup(currentProgress, maxProgress);
            _targetPointer.Setup(target, camera);
        }

        public void UpdateProgress(float progress)
        {
            _progressBarView.Show(progress);
        }

        public void Hide()
        {
            _progressBarView.gameObject.SetActive(false);
            _targetPointer.gameObject.SetActive(false);
            _capturePointLabel.gameObject.SetActive(false);
        }
    }
}