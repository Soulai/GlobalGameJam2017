using UnityEngine;

namespace ExitGate
{
    public class ExitTrigger : MonoBehaviour
    {
        private Animator _animator;
        private GameObject _flash;

        private int _playersInProximity;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _flash = transform.FindChild("Flash Container").gameObject;
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
            _flash.SetActive(true);
            

            // TODO: Fire an event that ends the round
        }
    }
}