using UnityEngine;
using GameControl;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float CurrentHealth;

        private BackgroundRadiationTracker _backgroundRadiationTracker;

        private void Start()
        {
            _backgroundRadiationTracker = FindObjectOfType<BackgroundRadiationTracker>();
        }

        public void Update()
        {
            CurrentHealth = Mathf.Max(0.0f, CurrentHealth - _backgroundRadiationTracker.BackgroundRadiationLevel);
        }
    }
}