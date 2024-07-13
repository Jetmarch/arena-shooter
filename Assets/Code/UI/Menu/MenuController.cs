using UnityEditor;
using UnityEngine.SceneManagement;
using Zenject;

namespace ArenaShooter.UI
{
    public class MenuController : IInitializable, ILateDisposable
    {
        private MenuView _menuView;
        private ArmoryView _armoryView;

        public MenuController(MenuView menuView, ArmoryView armoryView)
        {
            _menuView = menuView;
            _armoryView = armoryView;
        }

        public void Initialize()
        {
            _menuView.StartGameBtn.onClick.AddListener(StartGame);
            _menuView.ExitGameBtn.onClick.AddListener(ExitGame);
            _menuView.ArmoryBtn.onClick.AddListener(OpenArmory);
            _menuView.Show();
        }

        public void LateDispose()
        {
            _menuView.StartGameBtn.onClick.RemoveListener(StartGame);
            _menuView.ExitGameBtn.onClick.RemoveListener(ExitGame);
            _menuView.ArmoryBtn.onClick.RemoveListener(OpenArmory);
        }

        private void StartGame()
        {
            //TODO: Передавать название сцены через конфигурации или что-то в этом духе
            SceneManager.LoadScene("ArenaScene");
        }

        private void ExitGame()
        {
#if !UNITY_EDITOR
            Application.Quit();
#elif UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }

        private void OpenArmory()
        {
            _menuView.Hide();
            _armoryView.Show();
        }
    }
}