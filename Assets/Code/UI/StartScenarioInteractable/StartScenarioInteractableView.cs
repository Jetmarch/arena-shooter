using TMPro;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class StartScenarioInteractableView : MonoBehaviour
    {
        [SerializeField]
        private UITargetPointerView _pointer;

        [SerializeField]
        private TextMeshProUGUI _interactionLabel;

        public void Show()
        {
            _pointer.gameObject.SetActive(true);
            _interactionLabel.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _pointer.gameObject.SetActive(false);
            _interactionLabel.gameObject.SetActive(false);
        }
    }
}