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
            float stickValue = Input.GetAxis(AxisPrefix + "-Horizontal");
            float delta = Mathf.Abs(stickValue) > Rotation_Threshold ? stickValue : 0.0f;
            float updatedY = _transform.eulerAngles.y + (delta * Maximum_Rotation_Speed);

            _transform.localRotation = Quaternion.Euler(_transform.eulerAngles.x, updatedY, _transform.eulerAngles.z);
        }

        private void HandleThrust()
        {
            Vector3 planarVector = new Vector3(_transform.forward.x, 0.0f, _transform.forward.z);
            Vector3 delta = planarVector.normalized * Input.GetAxis(AxisPrefix + "-Vertical") * Maximum_Movement_Speed;

            _rigidbody.velocity = new Vector3(delta.x, _rigidbody.velocity.y, delta.z);
        }

        private const float Rotation_Threshold = 0.25f;
        private const float Maximum_Rotation_Speed = 2.0f;
        private const float Maximum_Movement_Speed = 5.0f;
    }
}