using UnityEngine;
using UnityEngine.UI;

namespace ArenaShooter.UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _menuContainer;

        [SerializeField]
        private Button _startGameBtn;
        [SerializeField]
        private Button _armoryBtn;
        [SerializeField]
        private Button _exitGameBtn;

        public Button StartGameBtn { get { return _startGameBtn; } }
        public Button ArmoryBtn { get { return _armoryBtn; } }
        public Button ExitGameBtn { get { return _exitGameBtn; } }

        public void Show()
        {
            _menuContainer.SetActive(true);
        }

        public void Hide()
        {
            _menuContainer.SetActive(false);
        }
    }
}