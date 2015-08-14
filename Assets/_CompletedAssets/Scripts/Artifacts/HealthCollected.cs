using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class HealthCollected : MonoBehaviour
    {
        public const int ARTIFACT_AMOUNT = 25;
        GameObject player;                          // Reference to the player GameObject.
        PlayerHealth playerHealth;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            // If the entering collider is the player...
            if (other.gameObject == player)
            {
                playerHealth.Regeneration(ARTIFACT_AMOUNT);
                Destroy(gameObject);
            }
        }

        void Awake()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<PlayerHealth>();
            }
        }

    }

}

