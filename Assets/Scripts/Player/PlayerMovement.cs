using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody _rigidbody;

        public string AxisPrefix;
        public Vector3 InputVector;
        public bool Fire1;
        public bool Fire2;

        private void Start()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            InputVector = new Vector3(
                Input.GetAxis(AxisPrefix + "-Horizontal"), 0.0f, Input.GetAxis(AxisPrefix + "-Vertical"));

            if (Input.GetButtonDown(AxisPrefix + "-Fire1")) { Debug.Log(AxisPrefix + "Fire1"); }
            if (Input.GetButtonDown(AxisPrefix + "-Fire2")) { Debug.Log(AxisPrefix + "Fire2"); }
        }
    }
}