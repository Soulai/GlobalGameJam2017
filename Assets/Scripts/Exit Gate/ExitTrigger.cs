using UnityEngine;
using Player;

namespace ExitGate
{
    public class ExitTrigger : MonoBehaviour
    {
        private Animator _animator;
        private GameObject _flash;
        private EnemyBehaviour[] _enemyBehaviourControllers;
        private GameObject[] _players;

        private int _playersInProximity;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _flash = transform.FindChild("Flash Container").gameObject;

            _enemyBehaviourControllers = FindObjectsOfType<EnemyBehaviour>();
            _players = GameObject.FindGameObjectsWithTag("Player");
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
            
            foreach(GameObject player in _players)
            {
                player.transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, transform.position.z));
                player.GetComponent<PlayerActions>().Reset();
                player.GetComponent<PlayerActions>().enabled = false;
                player.GetComponent<PlayerHealth>().enabled = false;
            }

            foreach(EnemyBehaviour enemy in _enemyBehaviourControllers)
            {
                enemy.enabled = false;
            }
        }
    }
}