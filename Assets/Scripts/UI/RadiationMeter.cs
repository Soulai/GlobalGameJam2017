using UnityEngine;
using Player;

namespace UI
{
    public class RadiationMeter : MonoBehaviour
    {
        public GameObject LinkedAvatar;

        private Transform _transform;
        private PlayerHealth _playerHealth;
        private float _zeroPointRotation;

        private void Start()
        {
            _transform = transform;
            _playerHealth = LinkedAvatar.transform.GetComponentInChildren<PlayerHealth>();
            _zeroPointRotation = _transform.rotation.z;
        }

        public float Rads;

        private void Update()
        {
            Rads = _playerHealth.TotalRadiation;
        }
    }
}