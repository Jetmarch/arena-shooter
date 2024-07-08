using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaShooter.UI
{
    public class AnnouncementScreenView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _announceText;

        [SerializeField]
        private GameObject _announceBlock;

        [SerializeField]
        private Button _toMenuBtn;

        [SerializeField]
        private Button _restartBtn;

        public Button ToMenuBtn => _toMenuBtn;
        public Button RestartBtn => _restartBtn;

        public void ShowText(string text)
        {
            _announceText.text = text;
            _announceBlock.SetActive(true);

            _restartBtn.gameObject.SetActive(true);
            _toMenuBtn.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _announceBlock.SetActive(false);
            _restartBtn.gameObject.SetActive(false);
            _toMenuBtn.gameObject.SetActive(false);
        }
    }
}