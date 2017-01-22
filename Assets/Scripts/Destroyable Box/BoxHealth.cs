using UnityEngine;
using System;
using UnityEngine.AI;

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
            _solidModel = transform.FindChild("Solid");
            _breakableModel = transform.FindChild("Breakable");
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
                    Sound.SoundEffectPlayer.PlayPositionedSound("break-box", transform.position);
                }
                else
                {
                    Sound.SoundEffectPlayer.PlayPositionedSound("hit-box", transform.position);
                }
            }
        }

        private void HandleDestruction()
        {
			BoxDestroyedEvent.Fire(this);

            _collider.enabled = false;
			GetComponent<NavMeshObstacle>().enabled = false;
            _solidModel.gameObject.SetActive(false);
            _breakableModel.gameObject.SetActive(true);
        }
    }
}