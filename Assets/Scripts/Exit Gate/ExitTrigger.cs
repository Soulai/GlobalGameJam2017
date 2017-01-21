using UnityEngine;

namespace ExitGate
{
    public class ExitTrigger : MonoBehaviour
    {
        private Animator _animator;

        private int _playersInProximity;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Player")
            {
                _playersInProximity += 1;
                if (_playersInProximity == 2)
                {
                    TriggerLevelClearedSequence();
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.transform.tag == "Player")
            {
                _playersInProximity -= 1;
            }
        }

        private void TriggerLevelClearedSequence()
        {
            _animator.SetTrigger("Open");

            // TODO: Fire an event that ends the round
        }
    }
}