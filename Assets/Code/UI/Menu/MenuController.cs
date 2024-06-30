using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace ArenaShooter.UI
{
    public class MenuController : IInitializable, ILateDisposable
    {
        [SerializeField]
        private MenuView _menuView;

        //private WeaponryView _weaponryView;

        public MenuController(MenuView menuView)
        {
            _menuView = menuView;
        }

        public void Initialize()
        {
            _menuView.StartGameBtn.onClick.AddListener(StartGame);
            _menuView.ExitGameBtn.onClick.AddListener(ExitGame);
            _menuView.ArmoryBtn.onClick.AddListener(OpenArmory);
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
            Debug.LogWarning("Armory will be soon");
            //_weaponryView.Open();
        }
    }
}