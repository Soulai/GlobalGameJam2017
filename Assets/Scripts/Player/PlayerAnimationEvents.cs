using UnityEngine;

namespace Player
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        private Transform _transform;
        public GameObject DamageCollider;

        private void Start()
        {
            _transform = transform.parent;
        }

        private void ActivateDamageCollider()
        {
            DamageCollider.SetActive(true);
        }

        private void DeactivateDamageCollider()
        {
            DamageCollider.SetActive(false);
        }

        private void Footfall()
        {
            Sound.SoundEffectPlayer.PlayPositionedSound("footstep", _transform.position, Random.Range(0.9f, 1.1f));
        }

        private void Breathing()
        {
            Sound.SoundEffectPlayer.PlayPositionedSound("breath", _transform.position, Random.Range(0.95f, 1.05f));
        }
    }
}