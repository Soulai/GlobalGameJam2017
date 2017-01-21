using UnityEngine;

namespace RadiationSources
{
    public class RadiationHotspot : MonoBehaviour
    {
        [SerializeField]
        private float _maximumDamage;
        [SerializeField]
        private AnimationCurve _interpolationCurve;

        private Transform _transform;
        private float _colliderRadius;

        private void Start()
        {
            _transform = transform;
            _colliderRadius = GetComponent<SphereCollider>().radius;
        }

        public float GetDamageFromTargetPosition(Vector3 targetPosition)
        {
            float distance = Vector3.Distance(_transform.position, targetPosition);
            float interpolation = _interpolationCurve.Evaluate(1.0f - (distance / _colliderRadius));

            return interpolation * _maximumDamage;
        }
    }
}