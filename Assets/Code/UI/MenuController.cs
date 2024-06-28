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
            _menuView.OnStartGame += StartGame;
            _menuView.OnExitGame += ExitGame;
            _menuView.OnArmory += OpenArmory;
        }

        public void LateDispose()
        {
            _menuView.OnStartGame -= StartGame;
            _menuView.OnExitGame -= ExitGame;
            _menuView.OnArmory -= OpenArmory;
        }

        private void StartGame()
        {
            //TODO: ���������� �������� ����� ����� ������������ ��� ���-�� � ���� ����
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