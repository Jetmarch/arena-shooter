using TMPro;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class AnnouncementScreenView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _announceText;

        [SerializeField]
        private GameObject _announceBlock;

        public void ShowText(string text)
        {
            _announceText.text = text;
            _announceBlock.SetActive(true);
        }
    }
}