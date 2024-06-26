using UnityEngine;


namespace ArenaShooter.Components
{
    public class SpawnPointComponent : MonoBehaviour
    {
        [SerializeField]
        private float _spawnRadius = 5f;

        public Vector3 GetRandomPointInside()
        {
            var randomPoint = Random.insideUnitCircle * _spawnRadius;
            return new Vector3(transform.position.x + randomPoint.x, transform.position.y + randomPoint.y, 0f);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0.92f, 0.16f, 0.3f);
            Gizmos.DrawSphere(transform.position, _spawnRadius);
        }
#endif
    }
}