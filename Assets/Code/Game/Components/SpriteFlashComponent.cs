using System.Collections;
using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteFlashComponent : MonoBehaviour
    {
        [SerializeField]
        private Color _flashColor;

        [SerializeField]
        private float _flashDuration;

        private float _flashMaxAmount = 1f;
        private float _flashAmount;

        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        [ContextMenu("Flash sprite")]
        public void Flash()
        {
            StartCoroutine(FlashCoroutine());
        }

        private IEnumerator FlashCoroutine()
        {
            _spriteRenderer.color = _flashColor;

            yield return new WaitForSeconds(_flashDuration);

            _spriteRenderer.color = Color.white;
        }
    }
}