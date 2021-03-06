﻿using UnityEngine;
using Player;

namespace UI
{
    public class RadiationMeter : MonoBehaviour
    {
        public GameObject LinkedAvatar;

        private Transform _transform;
        private PlayerHealth _playerHealth;
        private Transform _playerTransform;
        private float _previousRadiation;

        private void Start()
        {
            _transform = transform;
            _playerTransform = LinkedAvatar.transform;
            _playerHealth = _playerTransform.GetComponentInChildren<PlayerHealth>();
            _previousRadiation = 0.0f;
        }

        private void Update()
        {
            _previousRadiation = Mathf.Clamp((_previousRadiation * 0.75f) + (_playerHealth.TotalRadiation * 0.25f), 0.0f, Maximum_Radiation);
            float angle = Zero_Rotation - ((Rotation_Range * _previousRadiation / Maximum_Radiation) + Random.Range(0.0f, 0.5f));

            _transform.localRotation = Quaternion.Euler(_transform.eulerAngles.x, _transform.eulerAngles.y, angle);

            if ((Random.Range(0.0f, Sound_Random_Chance) < ((_previousRadiation / Maximum_Radiation) * 100.0f)) &&
                (_playerHealth.CurrentHealth > 0.0f))
            {
                Sound.SoundEffectPlayer.PlayPositionedSound("counter-" + Random.Range(1, 4), _playerTransform.position, Random.Range(0.95f, 1.05f));
            }
        }

        private const float Zero_Rotation = 28.0f;
        private const float Rotation_Range = 58.0f;
        private const float Maximum_Radiation = 50.0f;
        private const float Sound_Random_Chance = 100.0f;
    }
}