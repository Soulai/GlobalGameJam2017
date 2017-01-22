using UnityEngine;
using UnityEngine.UI;
using Player;

namespace UI
{
    public class HealthSpinner : MonoBehaviour
    {
        public GameObject LinkedAvatar;

        private Transform _leftSideTransform;
        private Image _leftSideImage;
        private Transform _rightSideTransform;
        private Image _rightSideImage;
        private Transform _overlayTransform;
        private PlayerHealth _playerHealth;
        private float _maximumHealth;

        private void Start()
        {
            _leftSideTransform = transform.FindChild("Health Spinner Left");
            _leftSideImage = _leftSideTransform.gameObject.GetComponent<Image>();
            _rightSideTransform = transform.FindChild("Health Spinner Right");
            _rightSideImage = _rightSideTransform.gameObject.GetComponent<Image>();
            _overlayTransform = transform.FindChild("Health Spinner Overlay");

            _playerHealth = LinkedAvatar.transform.GetComponentInChildren<PlayerHealth>();
            _maximumHealth = _playerHealth.CurrentHealth;;
        }

        private void Update()
        {
            float interpolation = Mathf.Clamp01(_playerHealth.CurrentHealth / _maximumHealth);
            if (interpolation > 0.5f)
            {
                HandleHealthOverHalfHealth((interpolation - 0.5f) * 2.0f);
            }
            else
            {

                HandleUnderHalfHealth(interpolation * 2.0f);
            }
        }

        private void HandleHealthOverHalfHealth(float interpolation)
        {
            Color colour = Color.Lerp(Color.yellow, Color.green, interpolation);

            _leftSideImage.color = colour;

            if (!_rightSideTransform.gameObject.activeInHierarchy)
            {
                _rightSideTransform.gameObject.gameObject.SetActive(true);
            }
            _rightSideTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f * interpolation);
            _rightSideImage.color = colour;

            if (_overlayTransform.gameObject.activeInHierarchy)
            {
                _overlayTransform.gameObject.SetActive(false);
            }
        }

        private void HandleUnderHalfHealth(float interpolation)
        {
            Color colour = Color.Lerp(Color.red, Color.yellow, interpolation);

            _leftSideImage.color = colour;

            if (_rightSideTransform.gameObject.activeInHierarchy)
            {
                _rightSideTransform.gameObject.gameObject.SetActive(false);
            }

            if (!_overlayTransform.gameObject.activeInHierarchy)
            {
                _overlayTransform.gameObject.SetActive(true);
            }
            _overlayTransform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f * interpolation);
        }
    }
}