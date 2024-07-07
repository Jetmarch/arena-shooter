using UnityEngine;

namespace ArenaShooter.UI
{
    public class CapturePointScenarioActView : MonoBehaviour
    {
        [SerializeField]
        private ProgressBarView _progressBarView;

        [SerializeField]
        private UITargetPointerView _targetPointer;

        public void Setup(float currentProgress, float maxProgress, Transform target, Camera camera)
        {
            _progressBarView.gameObject.SetActive(true);
            _targetPointer.gameObject.SetActive(true);
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
        }
    }
}