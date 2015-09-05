using UnityEngine;

namespace CompleteProject
{
    public class EnemyManager : MonoBehaviour
    {
        public Transform playerTransform;
        public PlayerHealth playerHealth;     
        public GameObject enemy;              
        public float spawnTime = 3f;          
        public Transform[] spawnPoints;       


        void Start ()
        {
            // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
            InvokeRepeating ("Spawn", spawnTime, spawnTime);
        }


        void Spawn ()
        {
            //If player has not been initialized yet
            if (playerHealth == null || playerTransform == null)
            {
                return;
            }

            // If the player has no health left...
            if(playerHealth.currentHealth <= 0f)
            {
                // ... exit the function.
                return;
            }

            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range (0, spawnPoints.Length);

            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            GameObject enemyObject = Network.Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation, 0) as GameObject;

            if (enemyObject != null)
            {
                EnemyMovement enemyMovement = enemyObject.GetComponent<EnemyMovement>();
                if (enemyMovement != null)
                {
                    enemyMovement.playerHealth = playerHealth;
                    enemyMovement.player = playerTransform;
                }
            }

        }
    }
}