using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    GameObject door;
    Animation anim;
    AnimationClip clip;
    void Awake()
    {
        
    }
    
    // Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        door = GameObject.FindGameObjectWithTag("SF_Door");
        
        anim = door.GetComponent<Animation>();
        anim.Play("open");
        
    }

    void OnTriggerExit(Collider other)
    {
        door = GameObject.FindGameObjectWithTag("SF_Door");
        anim = door.GetComponent<Animation>();
        anim.Play("close");
    }
}
