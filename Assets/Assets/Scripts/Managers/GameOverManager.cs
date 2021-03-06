﻿using UnityEngine;

namespace CompleteProject
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerHealth playerHealth;       // Reference to the player's health.


        Animator anim;                          // Reference to the animator component.


        void Awake ()
        {
            // Set up the reference.
            anim = GetComponent <Animator> ();
        }


        void Update ()
        {
            // If the player has run out of health...
            if(playerHealth!=null && playerHealth.currentHealth <= 0)
            {
                // ... tell the animator the game is over.
                anim.SetTrigger ("GameOver");

                //Destroying the player so the network object will be killed as well
                Network.Destroy(playerHealth.gameObject);
            }
        }
    }
}