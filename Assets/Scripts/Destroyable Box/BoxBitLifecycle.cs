using UnityEngine;

namespace DestroyableBox
{
    public class BoxBitLifecycle : MonoBehaviour
    {
        private float _remainingLifespan;
        private Material _material;

        private void Start()
        {
            _remainingLifespan = Lifespan;
            _material = GetComponentInChildren<MeshRenderer>().material;
        }

        private void Update()
        {
            _remainingLifespan -= Time.deltaTime;
            _material.color = Color.Lerp(Color.clear, Color.white, Mathf.Clamp01(_remainingLifespan / Fade_Point));

            if (_remainingLifespan <= 0.0f)
            {
                Destroy(gameObject);
            }
        }

        private const float Lifespan = 5.0f;
        private const float Fade_Point = 2.0f;
    }
}
