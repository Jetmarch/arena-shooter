using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class ScorePointsView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreText;


        public void Show(string scoreText)
        {
            _scoreText.text = scoreText;
        }
    }
}