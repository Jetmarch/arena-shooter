using DG.Tweening;
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
        [SerializeField]
        protected Image _backgroundImage;
        [SerializeField]
        protected Color _selectedColor;
        [SerializeField]
        protected Color _defaultColor = Color.white;
        [SerializeField]
        private float _colorChangeDuration = 0.3f;

        public virtual void Setup(IItemPresenter presenter)
        {
            _image.sprite = presenter.Sprite;
            _name.text = presenter.Name;
            _description.text = presenter.Description;
            _image.SetNativeSize();
        }

        public void SelectItemAnimation()
        {
            _backgroundImage.DOComplete();
            _backgroundImage.DOBlendableColor(_selectedColor, _colorChangeDuration);
        }

        public void DeselectItemAnimation()
        {
            _backgroundImage.DOComplete();
            _backgroundImage.DOBlendableColor(_defaultColor, _colorChangeDuration);
        }
    }
}