using UnityEngine;

namespace ArenaShooter.Weapons
{
    public class WeaponFacade : MonoBehaviour
    {
        [SerializeField]
        private AmmoClipStorage _ammoClipStorage;

        public AmmoClipStorage AmmoClipStorage { get { return _ammoClipStorage; } }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _ammoClipStorage = GetComponent<AmmoClipStorage>();
        }
#endif
    }
}