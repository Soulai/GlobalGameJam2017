using UnityEngine;
using GameControl;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float CurrentHealth; //{ get; private set; }

        private BackgroundRadiationTracker _backgroundRadiationTracker;

        private void Start()
        {
            _backgroundRadiationTracker = FindObjectOfType<BackgroundRadiationTracker>();

            CurrentHealth = Initial_Health;
        }

        public void Update()
        {
            CurrentHealth = Mathf.Max(0.0f, CurrentHealth - _backgroundRadiationTracker.BackgroundRadiationLevel);
        }

        private const float Initial_Health = 10000.0f;
    }
}