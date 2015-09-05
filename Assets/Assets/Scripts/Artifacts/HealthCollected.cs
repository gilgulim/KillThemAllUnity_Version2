using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class HealthCollected : MonoBehaviour
    {
        public const int ARTIFACT_AMOUNT = 25;


        void OnTriggerEnter(Collider other)
        {
            // If the entering collider is the player...
            if (other.tag == "Player")
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

                playerHealth.Regeneration(ARTIFACT_AMOUNT);
                Destroy(gameObject);
            }
        }
    }

}

