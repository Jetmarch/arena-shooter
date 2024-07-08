using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ArenaShooter.UI
{
    public class AnnouncementScreenButtonsController : IInitializable, ILateDisposable
    {
        private AnnouncementScreenView _view;
        private SceneNameProvider _sceneNameProvider;

        public AnnouncementScreenButtonsController(AnnouncementScreenView view, SceneNameProvider sceneNameProvider)
        {
            _view = view;
            _sceneNameProvider = sceneNameProvider;
        }

        public void Initialize()
        {
            _view.ToMenuBtn.onClick.AddListener(LoadMenuScene);
            _view.RestartBtn.onClick.AddListener(RestartArenaScene);
        }

        public void LateDispose()
        {
            _view.ToMenuBtn.onClick.RemoveListener(LoadMenuScene);
            _view.RestartBtn.onClick.RemoveListener(RestartArenaScene);
        }

        private void LoadMenuScene()
        {
            SceneManager.LoadScene(_sceneNameProvider.MenuSceneName);
        }

        private void RestartArenaScene()
        {
            SceneManager.LoadScene(_sceneNameProvider.ArenaSceneName);
        }
    }

    
}