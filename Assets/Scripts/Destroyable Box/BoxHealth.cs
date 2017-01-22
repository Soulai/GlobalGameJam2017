using UnityEngine;
using System;

namespace DestroyableBox
{
    public class BoxHealth : MonoBehaviour
    {
        private Transform _solidModel;
        private Transform _breakableModel;
        private BoxCollider _collider;

        public event Action<BoxHealth> BoxDestroyedEvent;

        public int HitsToDestroy;

        private void Start()
        {
            _solidModel = transform.FindChild("Solid Box");
            _breakableModel = transform.FindChild("Breakable Box");
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
            _solidModel.gameObject.SetActive(false);
            _breakableModel.gameObject.SetActive(true);
        }
    }
}