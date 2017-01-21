using UnityEngine;
using System;

namespace DestroyableBox
{
    public class BoxHealth : MonoBehaviour
    {
		public event Action<BoxHealth> BoxDestroyedEvent;

        public int HitsToDestroy;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "DamageCollider")
            {
                HitsToDestroy -= 1;
                if (HitsToDestroy < 1)
                {
                    HandleDestruction();
                }
            }
        }

        private void HandleDestruction()
        {
            // Just switch it off for now...
			Renderer[] renderers = GetComponentsInChildren<Renderer>();
			foreach (Renderer renderer in renderers)
			{
				renderer.enabled = false;
			}
			BoxDestroyedEvent.Fire(this);
        }
    }
}