using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody _rigidbody;
        private Animator _animator;

        public string AxisPrefix;

        private void Start()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
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
            float stickValue = Input.GetAxis(AxisPrefix + "-Vertical");
            float delta = Mathf.Abs(stickValue) > Movement_Threshold ? stickValue : 0.0f;
            Vector3 planarVector = new Vector3(_transform.forward.x, 0.0f, _transform.forward.z);
            Vector3 movementVector = planarVector.normalized * delta * Maximum_Movement_Speed;

            _animator.SetBool("IsMoving", delta > Movement_Threshold);

            _rigidbody.velocity = new Vector3(movementVector.x, _rigidbody.velocity.y, movementVector.z);
        }

        private const float Rotation_Threshold = 0.25f;
        private const float Maximum_Rotation_Speed = 2.0f;
        private const float Movement_Threshold = 0.1f;
        private const float Maximum_Movement_Speed = 5.0f;
    }
}