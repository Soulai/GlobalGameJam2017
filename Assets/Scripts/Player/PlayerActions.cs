using UnityEngine;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody _rigidbody;
        private Animator _animator;

        public bool PunchInProgress { private get; set; }

        public string AxisPrefix;

        private void Start()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();

            PunchInProgress = false;
        }

        private void Update()
        {
            HandleRotation();
            HandleThrust();

            if ((Input.GetButtonDown(AxisPrefix + "-Fire1")) && (!PunchInProgress))
            {
                Punch();
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

            if ((Mathf.Abs(stickValue) < Movement_Threshold) || (PunchInProgress))
            {
                stickValue = 0.0f;
            }

            float delta = Mathf.Clamp(stickValue * Maximum_Running_Speed, -Maximum_Walking_Speed, Maximum_Running_Speed);

            Vector3 planarVector = new Vector3(_transform.forward.x, 0.0f, _transform.forward.z);
            Vector3 movementVector = planarVector.normalized * delta;

            _animator.SetBool("Walking Forward", delta > Movement_Threshold);
            _animator.SetBool("Running", delta > Maximum_Walking_Speed);
            _animator.SetBool("Walking Backward", delta < -Movement_Threshold);

            _rigidbody.velocity = new Vector3(movementVector.x, _rigidbody.velocity.y, movementVector.z);
        }

        private void Punch()
        {
            PunchInProgress = true;
            _animator.SetBool("Walking Forward", false);
            _animator.SetBool("Running", false);
            _animator.SetBool("Walking Backward", false);
            _animator.SetTrigger("Attacking");
        }

        private const float Rotation_Threshold = 0.25f;
        private const float Maximum_Rotation_Speed = 2.0f;
        private const float Movement_Threshold = 0.1f;
        private const float Maximum_Walking_Speed = 2.0f;
        private const float Maximum_Running_Speed = 5.0f;
    }
}