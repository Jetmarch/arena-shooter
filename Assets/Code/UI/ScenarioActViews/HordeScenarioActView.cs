using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class HordeScenarioActView : MonoBehaviour
    {
        [SerializeField]
        private ProgressBarView _progressBarView;
        

        public void ShowProgress(float progress)
        {
            _progressBarView.Show(progress);
        }
    }
}