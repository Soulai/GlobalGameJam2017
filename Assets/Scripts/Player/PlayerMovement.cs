using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody _rigidbody;

        public string AxisPrefix;

        private void Start()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            HandleRotation();
            HandleThrust();
        }

        private void HandleRotation()
        {

        }

        private void HandleThrust()
        {
            Vector3 planarVector = new Vector3(_transform.forward.x, 0.0f, _transform.forward.z);
            Vector3 delta = planarVector.normalized * Input.GetAxis(AxisPrefix + "-Vertical") * Maximum_Movement_Speed;

            _rigidbody.velocity = new Vector3(delta.x, _rigidbody.velocity.y, delta.z);
        }

        private const float Maximum_Movement_Speed = 5.0f;
    }
}