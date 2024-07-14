using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaShooter.UI
{
    public class VictoryScreenView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _announceText;

        [SerializeField]
        private GameObject _container;

        [SerializeField]
        private Button _toMenuBtn;

        [SerializeField]
        private Button _restartBtn;

        [SerializeField]
        private Button _nextLevelBtn;

        public Button ToMenuBtn => _toMenuBtn;
        public Button RestartBtn => _restartBtn;
        public Button NextLevelBtn => _nextLevelBtn;

        public void Show(string text)
        {
            _announceText.text = text;
            _container.SetActive(true);
        }

        public void Hide()
        {
            _container.SetActive(false);
        }
    }
}