using ArenaShooter.Inputs;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.AI
{
    /// <summary>
    /// Управляет жизненным циклом ботов: создает, хранит, удаляет
    /// </summary>
    public class AIManager : MonoBehaviour
    {
        [SerializeField]
        private LinkedList<AIInputController> _botInputControllers;
        
        public IReadOnlyCollection<AIInputController> Bots { get {  return _botInputControllers; } }

        public GameObject CreateBot()
        {
            return null;
        }


        public void DestroyBot()
        {

        }
    }
}