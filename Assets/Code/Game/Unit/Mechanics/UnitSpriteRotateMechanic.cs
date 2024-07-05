using UnityEngine;

namespace ArenaShooter.Units
{
    public class UnitSpriteRotateMechanic : MonoBehaviour
    {
        [SerializeField]
        private Animator _unitAnimator;

        public void Rotate(float angle)
        {
            _unitAnimator.SetFloat("Angle", angle);
        }
    }
}