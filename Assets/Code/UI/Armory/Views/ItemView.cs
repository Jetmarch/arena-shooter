
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArenaShooter.UI
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField]
        protected Image _image;
        [SerializeField]
        protected TextMeshProUGUI _name;
        [SerializeField]
        protected TextMeshProUGUI _description;

        public virtual void Setup(IItemPresenter presenter)
        {
            _image.sprite = presenter.Sprite;
            _name.text = presenter.Name;
            _description.text = presenter.Description;
            _image.SetNativeSize();
        }
    }
}