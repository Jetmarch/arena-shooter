using ArenaShooter.Components;
using ArenaShooter.Units;
using System;
using UnityEngine;

namespace ArenaShooter.Weapons.Projectiles
{
    [RequireComponent(typeof(Trigger2DComponent))]
    public class ProjectileDamageController : MonoBehaviour
    {
        [SerializeField]
        private ProjectileConditionContainer _conditionContainer;

        [SerializeField]
        private Trigger2DComponent _triggerComponent;

        public event Action<GameObject> HitGameObject;

        private void Start()
        {
            _conditionContainer = GetComponent<ProjectileConditionContainer>();
            _triggerComponent = GetComponent<Trigger2DComponent>();

            _triggerComponent.TriggerOn += OnHit;
        }

        private void OnEnable()
        {
            if (_triggerComponent != null)
            {
                _triggerComponent.TriggerOn += OnHit;
            }
        }

        private void OnDisable()
        {
            _triggerComponent.TriggerOn -= OnHit;
        }

        private void OnHit(Collider2D obj)
        {
            Debug.Log($"Hit something: {obj.name}");
            HitGameObject?.Invoke(obj.gameObject);

            var unitConditionContainer = obj.gameObject.GetComponent<UnitConditionContainer>();
            if (unitConditionContainer != null)
            {
                Debug.Log($"Hit unit {unitConditionContainer.gameObject.name}");
            }
        }
    }
}