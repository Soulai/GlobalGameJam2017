using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private bool _swingInProgress;

        public string AxisPrefix;

        private void Start()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();

            _swingInProgress = false;
        }

        private void Update()
        {
            HandleRotation();
            HandleThrust();

            if ((Input.GetButtonDown(AxisPrefix + "-Fire1")) && (!_swingInProgress))
            {
                _swingInProgress = true;
            }
        }

        private void HandleRotation()
        {
            float stickValue = Input.GetAxis(AxisPrefix + "-Horizontal");

            stickValue = Mathf.Abs(stickValue) > Rotation_Threshold ? stickValue : 0.0f;
            float updatedY = _transform.eulerAngles.y + (stickValue * Maximum_Rotation_Speed);

            _transform.localRotation = Quaternion.Euler(_transform.eulerAngles.x, updatedY, _transform.eulerAngles.z);
        }

        private void HandleThrust()
        {
            float stickValue = Input.GetAxis(AxisPrefix + "-Vertical");

            if ((Mathf.Abs(stickValue) < Movement_Threshold) || (_swingInProgress))
            {
                stickValue = 0.0f;
            }

            Vector3 planarVector = new Vector3(_transform.forward.x, 0.0f, _transform.forward.z);
            Vector3 movementVector = planarVector.normalized * stickValue * Maximum_Movement_Speed;

            _animator.SetBool("IsMoving", stickValue > Movement_Threshold);

            _rigidbody.velocity = new Vector3(movementVector.x, _rigidbody.velocity.y, movementVector.z);
        }

        private const float Rotation_Threshold = 0.25f;
        private const float Maximum_Rotation_Speed = 2.0f;
        private const float Movement_Threshold = 0.1f;
        private const float Maximum_Movement_Speed = 5.0f;
    }
}