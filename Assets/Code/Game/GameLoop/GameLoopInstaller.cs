using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaShooter
{
    [RequireComponent(typeof(GameLoopManager))]
    [Obsolete]
    public class GameLoopInstaller : MonoBehaviour
    {
        private void Start()
        {
            //var gameLoopManager = GetComponent<GameLoopManager>();
            //var gameLoopListeners = GetComponentsInChildren<IGameLoopListener>();

            //foreach (var listener in gameLoopListeners)
            //{
            //    gameLoopManager.AddListener(listener);
            //}
        }
    }
}