using UnityEngine;

namespace Player
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        public GameObject DamageCollider;

        private void ActivateDamageCollider()
        {
            DamageCollider.SetActive(true);
        }

        private void DeactivateDamageCollider()
        {
            DamageCollider.SetActive(false);
        }
    }
}