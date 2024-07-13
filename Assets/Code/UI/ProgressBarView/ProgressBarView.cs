using UnityEngine;
using UnityEngine.UI;

namespace ArenaShooter.UI
{
    public class ProgressBarView : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        public void Setup(float currentValue, float maxValue)
        {
            _slider.maxValue = maxValue;
            Show(currentValue);
        }

        public void Show(float currentValue)
        {
            _slider.value = currentValue;
        }
    }
}