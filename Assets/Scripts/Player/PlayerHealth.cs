using System.Collections.Generic;
using UnityEngine;
using RadiationSources;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float CurrentHealth;
        public float TotalRadiation;

        private Transform _transform;
        private List<RadiationHotspot> _nearbyRadiationGenerators;
        private BackgroundRadiationGenerator _backgroundRadiationGenerator;

        private void Start()
        {
            _transform = transform;
            _backgroundRadiationGenerator = FindObjectOfType<BackgroundRadiationGenerator>();

            _nearbyRadiationGenerators = new List<RadiationHotspot>();
        }

        private void Update()
        {
            float generatedRadiation = GetRadiationFromNearbyGenerators();

            TotalRadiation = _backgroundRadiationGenerator.BackgroundRadiationLevel + generatedRadiation;
            CurrentHealth = Mathf.Max(CurrentHealth - TotalRadiation, 0.0f);
        }

        private float GetRadiationFromNearbyGenerators()
        {
            float radiation = 0.0f;

            if (_nearbyRadiationGenerators.Count > 0)
            {
                for (int i=0; i<_nearbyRadiationGenerators.Count; i++)
                {
                    radiation += _nearbyRadiationGenerators[i].GetDamageFromTargetPosition(_transform.position);
                }
            }

            return radiation;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "RadiationGenerator")
            {
                RadiationHotspot radiationHotspot = collider.transform.GetComponent<RadiationHotspot>();
                if ((radiationHotspot != null) && (!_nearbyRadiationGenerators.Contains(radiationHotspot)))
                {
                    _nearbyRadiationGenerators.Add(radiationHotspot);
                }
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.tag == "RadiationGenerator")
            {
                RadiationHotspot radiationHotspot = collider.transform.GetComponent<RadiationHotspot>();
                if ((radiationHotspot != null) && (_nearbyRadiationGenerators.Contains(radiationHotspot)))
                {
                    _nearbyRadiationGenerators.Remove(radiationHotspot);
                }
            }
        }
    }
}