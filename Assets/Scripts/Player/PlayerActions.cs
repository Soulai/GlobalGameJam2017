using UnityEngine;

namespace Player
{
    public class PlayerActions : MonoBehaviour
    {
		[SerializeField]
		private float rotationThreshold = 0.25f;
		[SerializeField]
		private float maximumRotationSpeed = 2.0f;
		[SerializeField]
		private float movementThreshold = 0.1f;
		[SerializeField]
		private float maximumWalkingSpeed = 2.0f;
		[SerializeField]
		private float maximumRunningSpeed = 5.0f;

        private Transform _transform;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private PlayerHealth _healthTracker;

        private bool _isAlive;

		public float MaximumRunningSpeed
		{
			get
			{
				return maximumRunningSpeed;
			}
		}

        public bool PunchInProgress { private get; set; }

        public string AxisPrefix;

        private void Start()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponentInChildren<Animator>();
            _healthTracker = GetComponent<PlayerHealth>();

            _isAlive = true;

            PunchInProgress = false;
        }

        private void Update()
        {
            if (_isAlive)
            {
                HandleRotation();
                HandleThrust();
                HandlePunching();
                HandleDying();
            }
        }

        private void HandleRotation()
        {
            float stickValue = Input.GetAxis(AxisPrefix + "-Horizontal");

            stickValue = Mathf.Abs(stickValue) > rotationThreshold ? stickValue : 0.0f;
            float updatedY = _transform.eulerAngles.y + (stickValue * maximumRotationSpeed);

            _transform.localRotation = Quaternion.Euler(_transform.eulerAngles.x, updatedY, _transform.eulerAngles.z);
        }

        public float stickValue;

        private void HandleThrust()
        {
            //float
                stickValue = Input.GetAxis(AxisPrefix + "-Vertical");

            if ((Mathf.Abs(stickValue) < movementThreshold) || (PunchInProgress))
            {
                stickValue = 0.0f;
            }

            float delta = Mathf.Clamp(stickValue * maximumRunningSpeed, -maximumWalkingSpeed, maximumRunningSpeed);

            Vector3 planarVector = new Vector3(_transform.forward.x, 0.0f, _transform.forward.z);
            Vector3 movementVector = planarVector.normalized * delta;

            _animator.SetBool("Walking Forward", delta > movementThreshold);
            _animator.SetBool("Running", delta > maximumWalkingSpeed);
            _animator.SetBool("Walking Backward", delta < -movementThreshold);

            _rigidbody.velocity = new Vector3(movementVector.x, _rigidbody.velocity.y, movementVector.z);
        }

        private void HandlePunching()
        {
            if ((Input.GetButtonDown(AxisPrefix + "-Fire1")) && (!PunchInProgress))
            {
                PunchInProgress = true;
                _animator.SetBool("Walking Forward", false);
                _animator.SetBool("Running", false);
                _animator.SetBool("Walking Backward", false);
                _animator.SetTrigger("Attacking");
            }
        }

        private void HandleDying()
        {
            if ((_isAlive) && (_healthTracker.CurrentHealth <= 0))
            {
                _isAlive = false;
                _animator.SetTrigger("HasDied");
            }
        }
    }
}