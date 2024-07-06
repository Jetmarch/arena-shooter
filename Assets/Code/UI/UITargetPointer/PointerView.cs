using DG.Tweening;
using UnityEngine;

namespace ArenaShooter.UI
{
    public class PointerView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _pointer;
        [SerializeField]
        private Vector3 _pointerPunchScale;
        [SerializeField]
        private float _pointerPunchDuration;

        private Tween _pointerAnim;

        public void SetPosition(Vector3 position)
        {
            _pointer.transform.position = position;
        }

        public void Activate()
        {
            if (_pointerAnim != null) return;

            _pointerAnim = _pointer.transform.DOPunchScale(_pointerPunchScale, _pointerPunchDuration).SetLoops(-1);
        }

        public void Deactivate()
        {
            _pointerAnim.Rewind();
            _pointerAnim.Kill();
            _pointerAnim = null;
        }
    }
}