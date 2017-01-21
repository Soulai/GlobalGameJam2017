using UnityEngine;

namespace GameControl
{
    public class BackgroundRadiationTracker : MonoBehaviour
    {
        private float _levelTimeElapsed;

        public float TimeToRadiationPeak;

        public float BackgroundRadiationLevel; // { get; private set; }

        private void Start()
        {
            TimeToRadiationPeak = TimeToRadiationPeak <= 0.0f ? 20.0f : TimeToRadiationPeak;
            _levelTimeElapsed = 0.0f;
        }

        private void Update()
        {
            _levelTimeElapsed += Time.deltaTime;
            BackgroundRadiationLevel = Maximum_Background_Radiation * Mathf.Clamp01(_levelTimeElapsed / TimeToRadiationPeak);
        }

        private const float Maximum_Background_Radiation = 5.0f;
    }
}