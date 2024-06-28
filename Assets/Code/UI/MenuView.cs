using System;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class MenuView : MonoBehaviour
    {
        public event Action OnStartGame;
        public event Action OnArmory;
        public event Action OnExitGame;

        public void OnStartGameBtn_UnityEditor()
        {
            OnStartGame?.Invoke();
        }

        public void OnArmory_UnityEditor()
        {
            OnArmory?.Invoke();
        }

        public void OnExitGame_UnityEditor()
        {
            OnExitGame?.Invoke();
        }
    }
}