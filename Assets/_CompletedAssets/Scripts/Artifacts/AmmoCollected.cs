using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class AmmoCollected : MonoBehaviour
    {
        public const int ARTIFACT_AMOUNT = 75;
        GameObject player;                          // Reference to the player GameObject.
        PlayerShooting playerShooting;
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
                playerShooting.AddAmmo(ARTIFACT_AMOUNT);
                Destroy(gameObject);
            }
        }

        void Awake()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerShooting = player.GetComponentInChildren<PlayerShooting>();
            }
        }
            
    }

}

