using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    public class ProjectileConditionContainer : MonoBehaviour
    {
        [SerializeField]
        private int _damage;
        [SerializeField]
        private int _moveSpeed;
        
        public int Damage { get => _damage; set => _damage = value; }
        public int MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    }
}