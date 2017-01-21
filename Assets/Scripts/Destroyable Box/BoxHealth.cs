using UnityEngine;
using System;

namespace DestroyableBox
{
    public class BoxHealth : MonoBehaviour
    {
        private Transform _solidModels;
        private Transform _breakableModels;
        private BoxCollider _collider;

        public event Action<BoxHealth> BoxDestroyedEvent;

        public int HitsToDestroy;

        private void Start()
        {
            _solidModels = transform.FindChild("Solid Models");
            _breakableModels = transform.FindChild("Exploding Models");
            _collider = GetComponent<BoxCollider>();
        }

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
			BoxDestroyedEvent.Fire(this);

            _collider.enabled = false;
            _solidModels.gameObject.SetActive(false);
            _breakableModels.gameObject.SetActive(true);
        }
    }
}