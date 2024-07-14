using UnityEngine.SceneManagement;
using Zenject;

namespace ArenaShooter.UI
{
    public class VictoryScreenButtonsController : IInitializable, ILateDisposable
    {
        private VictoryScreenView _view;
        private SceneNameProvider _sceneNameProvider;

        public VictoryScreenButtonsController(VictoryScreenView view, SceneNameProvider sceneNameProvider)
        {
            _view = view;
            _sceneNameProvider = sceneNameProvider;
        }

        public void Initialize()
        {
            _view.ToMenuBtn.onClick.AddListener(LoadMenuScene);
            _view.RestartBtn.onClick.AddListener(RestartArenaScene);
            _view.NextLevelBtn.onClick.AddListener(NextArenaScene);
        }

        public void LateDispose()
        {
            _view.ToMenuBtn.onClick.RemoveListener(LoadMenuScene);
            _view.RestartBtn.onClick.RemoveListener(RestartArenaScene);
            _view.NextLevelBtn.onClick.RemoveListener(NextArenaScene);
        }

        private void LoadMenuScene()
        {
            SceneManager.LoadScene(_sceneNameProvider.MenuSceneName);
        }

        private void RestartArenaScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void NextArenaScene()
        {
            SceneManager.LoadScene(_sceneNameProvider.NextSceneName);
        }
    }


}