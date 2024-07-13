using Zenject;


namespace ArenaShooter.UI
{
    public class ArmoryController : IInitializable, ILateDisposable
    {
        private ArmoryView _armoryView;
        private MenuView _menuView;

        public ArmoryController(ArmoryView armoryView, MenuView menuView)
        {
            _armoryView = armoryView;
            _menuView = menuView;
        }

        public void Initialize()
        {
            _armoryView.BackBtn.onClick.AddListener(OpenMenu);
        }

        public void LateDispose()
        {
            _armoryView.BackBtn.onClick.RemoveListener(OpenMenu);
        }

        private void OpenMenu()
        {
            _armoryView.Hide();
            _menuView.Show();
        }
    }
}