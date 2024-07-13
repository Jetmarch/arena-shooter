using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaShooter.UI
{
    public class ArmoryView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _armoryContainer;

        [SerializeField]
        private Button _backBtn;

        public Button BackBtn { get { return _backBtn; } }


        public void Show()
        {
            _armoryContainer.SetActive(true);
        }

        public void Hide()
        {
            _armoryContainer?.SetActive(false);
        }
    }
}