using System;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaShooter.UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField]
        private Button _startGameBtn;
        [SerializeField]
        private Button _armoryBtn;
        [SerializeField]
        private Button _exitGameBtn;

        public Button StartGameBtn { get { return _startGameBtn; } }
        public Button ArmoryBtn { get { return _armoryBtn; } }
        public Button ExitGameBtn { get { return _exitGameBtn; } }
    }
}