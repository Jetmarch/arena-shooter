using ArenaShooter.Inputs;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ArenaShooter.UI
{
    public class GameMenuController : IInitializable, ILateDisposable
    {
        private IMenuInputProvider _inputProvider;
        private GameMenuView _menuView;

        public GameMenuController(IMenuInputProvider inputProvider, GameMenuView menuView)
        {
            _inputProvider = inputProvider;
            _menuView = menuView;
        }

        public void Initialize()
        {
            _inputProvider.OnMenu += _menuView.Toggle;
            _menuView.RestartGameBtn.onClick.AddListener(RestartGame);
            _menuView.ExitGameBtn.onClick.AddListener(ExitGame);
        }

        public void LateDispose()
        {
            _inputProvider.OnMenu -= _menuView.Toggle;
            _menuView.RestartGameBtn.onClick.RemoveListener(RestartGame);
            _menuView.ExitGameBtn.onClick.RemoveListener(ExitGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void ExitGame()
        {
#if !UNITY_EDITOR
            Application.Quit();
#elif UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
    }
}