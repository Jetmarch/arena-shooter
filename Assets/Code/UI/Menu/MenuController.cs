using UnityEditor;
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
using UnityEngine;
#endif
using UnityEngine.SceneManagement;
using Zenject;

namespace ArenaShooter.UI
{
    public class MenuController : IInitializable, ILateDisposable
    {
        private MenuView _menuView;
        private ArmoryView _armoryView;
        private string _gameSceneName;

        public MenuController(MenuView menuView, ArmoryView armoryView, string gameSceneName)
        {
            _menuView = menuView;
            _armoryView = armoryView;
            _gameSceneName = gameSceneName;
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
            SceneManager.LoadScene(_gameSceneName);
        }

        private void ExitGame()
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
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