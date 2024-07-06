using UnityEngine;

namespace ArenaShooter.UI
{
    public class CapturePointScenarioActView : MonoBehaviour
    {
        [SerializeField]
        private ProgressBarView _progressBarView;

        private UITargetPointerView _targetPointer;

        public void Setup(float currentProgress, float maxProgress, Transform target, Camera camera)
        {
            _progressBarView.Setup(currentProgress, maxProgress);
            _targetPointer.Setup(target, camera);
        }
    }
}