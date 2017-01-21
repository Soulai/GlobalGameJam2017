using UnityEngine;

namespace GameControl
{
    public class BackgroundRadiationTracker : MonoBehaviour
    {
        private float _levelTimeElapsed;

        [SerializeField]
        private float _timeToRadiationPeak = 20.0f;

        public float BackgroundRadiationLevel; // { get; private set; }

        private void Start()
        {
            _levelTimeElapsed = 0.0f;
        }

        private void Update()
        {
            _levelTimeElapsed += Time.deltaTime;
            BackgroundRadiationLevel = Maximum_Background_Radiation * Mathf.Clamp01(_levelTimeElapsed / _timeToRadiationPeak);
        }

        private const float Maximum_Background_Radiation = 5.0f;
    }
}