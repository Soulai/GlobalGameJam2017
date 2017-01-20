using UnityEngine;

namespace DestroyableBox
{
    public class BoxHealth : MonoBehaviour
    {
        public int HitsToDestroy;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Damage Collider")
            {
                HitsToDestroy -= 1;
                if (HitsToDestroy < 1)
                {
                    HandleDestruction();
                }
            }
        }

        private void HandleDestruction()
        {
            // Just switch it off for now...
            gameObject.SetActive(false);
        }
    }
}