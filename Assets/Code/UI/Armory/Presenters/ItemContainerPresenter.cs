using System.Collections.Generic;

namespace ArenaShooter.UI
{
    public class ItemContainerPresenter : IItemContainerPresenter
    {
        private List<ItemView> _itemViews;

        public ItemContainerPresenter(List<ItemView> weaponViews)
        {
            _itemViews = weaponViews;
        }

        public void ClearSelectedItem()
        {
            foreach(var item in _itemViews)
            {
                item.DeselectItemAnimation();
            }
        }
    }
}