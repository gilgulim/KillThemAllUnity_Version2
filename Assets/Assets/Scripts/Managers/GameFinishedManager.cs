using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameFinishedManager : MonoBehaviour {

    public Text winningText;
    private bool visible;
    private float deathTime;
    private AudioSource youWonAudioSource;
    private bool played;

	// Use this for initialization
	void Start () {
        visible = false;
        played = false;
        youWonAudioSource = GetComponent<AudioSource>();
  
   
	}
	
	// Update is called once per frame
	void Update () {
        if (winningText != null)
        {
            winningText.enabled = visible;
            if (visible)
            {
                if (!played)
                {
                    //youWonAudioSource.Play();
                    played = true;
                }

                deathTime += Time.deltaTime;
                if (deathTime > 2)
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.tag == "Player")
        {
            visible = true;
            deathTime = 0;
        }
    }
}
