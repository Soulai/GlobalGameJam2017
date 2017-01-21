using UnityEngine;

namespace RadiationSources
{
    public class BackgroundRadiationGenerator : MonoBehaviour
    {
        private float _levelTimeElapsed;

        [SerializeField]
        private float _timeToRadiationPeak = 20.0f;
        [SerializeField]
        private float _maximumBackgroundRadiation = 5.0f;

        public float BackgroundRadiationLevel; // { get; private set; }

        private void Start()
        {
            _levelTimeElapsed = 0.0f;
        }

        private void Update()
        {
            _levelTimeElapsed += Time.deltaTime;
            BackgroundRadiationLevel = _maximumBackgroundRadiation * Mathf.Clamp01(_levelTimeElapsed / _timeToRadiationPeak);
        }
    }
}