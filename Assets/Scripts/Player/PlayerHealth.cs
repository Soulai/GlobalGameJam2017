using UnityEngine;
using RadiationSources;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float CurrentHealth;

        private BackgroundRadiationGenerator _backgroundRadiationGenerator;

        private void Start()
        {
            _backgroundRadiationGenerator = FindObjectOfType<BackgroundRadiationGenerator>();
        }

        public void Update()
        {
            CurrentHealth = Mathf.Max(0.0f, CurrentHealth - _backgroundRadiationGenerator.BackgroundRadiationLevel);
        }
    }
}