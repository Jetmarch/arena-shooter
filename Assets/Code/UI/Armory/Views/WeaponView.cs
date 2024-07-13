using ArenaShooter.Weapons;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ArenaShooter.UI
{
    public class WeaponView : ItemView, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private WeaponType _type;
        private IWeaponPresenter _presenter;
        public WeaponType Type { get { return _type; } set { _type = value; } }

        public override void Setup(IItemPresenter presenter)
        {
            var weaponPresenter = presenter as IWeaponPresenter;
            if (weaponPresenter == null)
            {
                throw new System.Exception($"Type mismatch! Presenter is not IWeaponPresenter. Actual type: {presenter.GetType().Name}");
            }
            base.Setup(presenter);
            _type = weaponPresenter.WeaponType;
            _presenter = weaponPresenter;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _presenter.SelectWeapon(_type);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _presenter.ShowDescription();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _presenter.HideDescription();
        }
    }
}