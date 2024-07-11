using ArenaShooter.Weapons;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ArenaShooter.UI
{
    public class WeaponView : ItemView, IPointerClickHandler
    {
        [SerializeField]
        private WeaponType _type;
        public WeaponType Type { get { return _type; } set { _type = value; } }

        private IWeaponPresenter _presenter;

        public override void Setup(IItemPresenter presenter)
        {
            var weaponPresenter = presenter as IWeaponPresenter;
            if(weaponPresenter == null)
            {
                throw new System.Exception($"Type mismatch! Presenter is not IWeaponPresenter. Actual type: {presenter.GetType().Name}");
            }
            base.Setup(presenter);
            _type = weaponPresenter.WeaponType;
            _presenter = weaponPresenter;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _presenter.ChooseWeapon(_type);
        }
    }
}