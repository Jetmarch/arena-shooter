using System;
using UnityEngine;

namespace ArenaShooter.UI
{
    [Serializable]
    public class UIPrefabContainer
    {
        [SerializeField]
        private GameObject _uiPrefab;
        [SerializeField]
        private Transform _uiParent;

        public GameObject UIPrefab { get { return _uiPrefab; } }
        public Transform UIParent { get { return _uiParent; } }
    }
}