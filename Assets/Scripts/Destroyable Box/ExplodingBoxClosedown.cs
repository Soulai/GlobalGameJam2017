using UnityEngine;

namespace DestroyableBox
{
    public class ExplodingBoxClosedown : MonoBehaviour
    {
        private SphereCollider _collider;
        private float _timeToClosedown;

        private void Start()
        {
            _collider = GetComponent<SphereCollider>();
            _timeToClosedown = Delay_Before_Destroy;
        }

        private void Update()
        {
            _timeToClosedown -= Time.deltaTime;
            if ((_collider.enabled) && (_timeToClosedown < Delay_Before_Collider_Disable))
            {
                _collider.enabled = false;
            }

            if (_timeToClosedown <= 0.0f)
            {
                Destroy(gameObject);
            }
        }

        private const float Delay_Before_Collider_Disable = 5.0f;
        private const float Delay_Before_Destroy = 5.5f;
    }
}
