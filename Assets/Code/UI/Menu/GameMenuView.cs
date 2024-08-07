using UnityEngine;
using UnityEngine.UI;

namespace ArenaShooter.UI
{
    public class GameMenuView : MonoBehaviour
    {
        [SerializeField]
        private Button _restartGameBtn;
        [SerializeField]
        private Button _resumeGameBtn;
        [SerializeField]
        private Button _toMenuGameBtn;
        [SerializeField]
        private Button _exitGameBtn;
        [SerializeField]
        private GameObject _menuContainer;

        public Button RestartGameBtn { get { return _restartGameBtn; } }
        public Button ExitGameBtn { get { return _exitGameBtn; } }
        public Button ResumeGameBtn { get { return _resumeGameBtn; } }
        public Button ToMenuGameBtn { get { return _toMenuGameBtn; } }

        private bool _isActive;

        public void Toggle()
        {
            if (_isActive)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public void Show()
        {
            _menuContainer?.SetActive(true);
            _isActive = true;
        }

        public void Hide()
        {
            _menuContainer?.SetActive(false);
            _isActive = false;
        }
    }
}