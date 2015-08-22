using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class AmmoCollected : MonoBehaviour
    {
        public const int ARTIFACT_AMOUNT = 75;

        void OnTriggerEnter(Collider other)
        {
            // If the entering collider is the player...
            if (other.tag == "Player")
            {
                PlayerShooting playerShooting = other.GetComponentInChildren<PlayerShooting>();

                if (playerShooting != null)
                {
                    playerShooting.AddAmmo(ARTIFACT_AMOUNT);
                    Destroy(gameObject);
                }
            }
        }

            
    }

}

