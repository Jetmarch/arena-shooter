using ArenaShooter.Inputs;
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
using UnityEngine;
#endif
using UnityEditor;
using UnityEngine.SceneManagement;
using Zenject;

namespace ArenaShooter.UI
{
    public class GameMenuController : IInitializable, ILateDisposable
    {
        private IMenuInputProvider _inputProvider;
        private GameMenuView _menuView;
        private GameLoopManager _gameLoopManager;
        private string _menuSceneName;

        public GameMenuController(IMenuInputProvider inputProvider, GameMenuView menuView, GameLoopManager gameLoopManager, string menuSceneName)
        {
            _inputProvider = inputProvider;
            _menuView = menuView;
            _gameLoopManager = gameLoopManager;
            _menuSceneName = menuSceneName;
        }

        public void Initialize()
        {
            _inputProvider.OnMenu += _menuView.Toggle;
            _menuView.RestartGameBtn.onClick.AddListener(RestartGame);
            _menuView.ExitGameBtn.onClick.AddListener(ExitGame);
            _menuView.ResumeGameBtn.onClick.AddListener(ResumeGame);
            _menuView.ToMenuGameBtn.onClick.AddListener(ToMenu);
        }

        public void LateDispose()
        {
            _inputProvider.OnMenu -= _menuView.Toggle;
            _menuView.RestartGameBtn.onClick.RemoveListener(RestartGame);
            _menuView.ExitGameBtn.onClick.RemoveListener(ExitGame);
            _menuView.ResumeGameBtn.onClick.RemoveListener(ResumeGame);
            _menuView.ToMenuGameBtn.onClick.RemoveListener(ToMenu);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void ResumeGame()
        {
            _gameLoopManager.ResumeGame();
            _menuView.Hide();
        }

        private void ToMenu()
        {
            SceneManager.LoadScene(_menuSceneName);
        }

        private void ExitGame()
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            Application.Quit();
#elif UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}