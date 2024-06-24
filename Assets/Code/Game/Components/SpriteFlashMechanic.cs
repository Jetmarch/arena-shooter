using System.Collections;
using UnityEngine;

namespace ArenaShooter.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteFlashMechanic : MonoBehaviour
    {
        [SerializeField]
        private Material _flashMaterial;

        [SerializeField]
        private float _flashDuration;


        private SpriteRenderer _spriteRenderer;
        private Material _originalMaterial;

        public void Construct(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
            _originalMaterial = _spriteRenderer.material;
        }

        public void Flash()
        {
            StartCoroutine(FlashCoroutine());
        }

        private IEnumerator FlashCoroutine()
        {
            _spriteRenderer.material = _flashMaterial;

            yield return new WaitForSeconds(_flashDuration);

            _spriteRenderer.material = _originalMaterial;
        }
    }
}