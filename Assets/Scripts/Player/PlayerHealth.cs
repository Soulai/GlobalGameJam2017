using System.Collections.Generic;
using UnityEngine;
using RadiationSources;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public float CurrentHealth;

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
            if (CurrentHealth > 0.0f)
            {
                ApplyDamageFromBackgroundRadiation();
                ApplyDamageFromNearbyRadiationGenerators();

                CurrentHealth = Mathf.Max(CurrentHealth, 0.0f);
            }
        }

        private void ApplyDamageFromBackgroundRadiation()
        {
            //CurrentHealth -= _backgroundRadiationGenerator.BackgroundRadiationLevel;
        }

        private void ApplyDamageFromNearbyRadiationGenerators()
        {
            if (_nearbyRadiationGenerators.Count > 0)
            {
                for (int i=0; i<_nearbyRadiationGenerators.Count; i++)
                {
                    CurrentHealth -= _nearbyRadiationGenerators[i].GetDamageFromTargetPosition(_transform.position);
                }
            }
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