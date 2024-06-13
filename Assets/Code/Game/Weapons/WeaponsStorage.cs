using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Weapons
{
    /// <summary>
    /// ’ранилище оружи€ дл€ персонажа
    /// ѕозвол€ет получить доступ к списку оружи€, добавить новое или удалить
    /// </summary>
    public class WeaponsStorage : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _weapons;

        public IReadOnlyCollection<GameObject> Weapons { get { return _weapons; } }
        public event Action OnWeaponsChanged;

        public void AddWeapon(GameObject weapon)
        {
            _weapons.Add(weapon);
            OnWeaponsChanged?.Invoke();
        }

        public void RemoveWeapon(GameObject weapon)
        {
            _weapons.Remove(weapon);
            OnWeaponsChanged?.Invoke();
        }
    }
}