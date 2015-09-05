using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class EnemyMovement : MonoBehaviour
    {
        public Transform player;             
        public PlayerHealth playerHealth;    
        EnemyHealth enemyHealth;        
        NavMeshAgent nav;               


        void Awake ()
        {
            // Set up the references.
            //player = GameObject.FindGameObjectWithTag ("Player").transform;
            //playerHealth = player.GetComponent <PlayerHealth> ();
            player = null;
            playerHealth = null;
            enemyHealth = GetComponent <EnemyHealth> ();
            nav = GetComponent <NavMeshAgent> ();
        }


        void Update ()
        {
            if (player != null && playerHealth != null)
            {
                // If the enemy and the player have health left...
                if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
                {
                    // ... set the destination of the nav mesh agent to the player.
                    nav.SetDestination(player.position);
                }
                // Otherwise...
                else
                {
                    // ... disable the nav mesh agent.
                    nav.enabled = false;
                }
            }
        }
    }
}