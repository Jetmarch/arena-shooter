using UnityEngine;


namespace ArenaShooter.Components
{
    public class DestroyAfterDelayComponent : MonoBehaviour
    {
        [SerializeField]
        private float _delayInSec = 5f;

        private void Start()
        {
            Destroy(gameObject, _delayInSec);
        }
    }
}